using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Ca2d.Toolkit
{
    public readonly struct Log : IDisposable
    {
        private const string kNoLabelAndContextName = "Anonymous";

        private const string kFallbackLabelName = "Fallback";
        
        private static HashSet<int> _loopDetector = new();
        
        private static StringBuilder _sb = new();
        
        #region ManagedRegionOperations

        private static Core _fallbackCore = new Core
        {
            ValidateNumber = 0,
            ParentIndex = 0,
            ParentValidateNumber = 0,
            Context = null,
            Label = kFallbackLabelName
        };

        private static List<Core> _core = new()
        {
            // This is the first element of AdvDebugCore and also as fallback options.
            _fallbackCore
        };

        #region Allocation

        private static List<Vector2Int> m_allocMap = new()
        {
            // The first element must be allocated.
            new Vector2Int(0, 1)
        };

        private static bool RangeOverlap(Vector2Int a, Vector2Int b)
        {
            var offset = a.x - b.x;
            switch (offset)
            {
                case < 0:
                {
                    var o = -offset;
                    var lenA = a.y - a.x;
                    return o < lenA;
                }
                case > 0:
                {
                    var o = offset;
                    var lenB = b.y - b.x;
                    return o < lenB;
                }
                default:
                    return true;
            }
        }
        

        private static bool IsIdUsed(int id)
        {
            return m_allocMap.Exists(range => id >= range.x && id < range.y);
        }

        private static int GetUnusedId()
        {
            var alloc = m_allocMap[0].y;
            if (alloc == int.MaxValue) throw new IndexOutOfRangeException("No free index can be exposed!");
            return alloc;
        }

        public static bool AllocId(int id)
        {
            if (id <= 0 || id == int.MaxValue) return false;
            var range = new Vector2Int(id, id + 1);
            
            for (var i = 1; i < m_allocMap.Count; i++)
            {
                var r = m_allocMap[i];
                var insert = i + 1;
                if (m_allocMap.Count != insert && RangeOverlap(r, range)) continue;

                // Get the reaction on merge.
                var mergePrev = false;
                var mergeNext = false;
                
                var before = m_allocMap[insert - 1];
                if (before.y == range.x) mergePrev = true;

                var after = m_allocMap.Count > insert ? m_allocMap[insert] : default;
                if (after.y != 0 && after.x == range.y) mergeNext = true;

                if (mergePrev & mergeNext)
                { 
                    m_allocMap[insert - 1] = new Vector2Int(before.x, after.y);
                    m_allocMap.RemoveAt(insert);
                }
                else if (mergePrev)
                {
                    m_allocMap[insert - 1] = new Vector2Int(before.x, range.y);
                }
                else if (mergeNext)
                {
                    m_allocMap[insert] = new Vector2Int(range.x, after.y);
                }
                else
                {
                    m_allocMap.Insert(insert, range);
                }

                return true;
            }

            return false;
        }

        public static bool FreeId(int id)
        {
            if (id <= 0 || id == int.MaxValue) return false;
            var range = new Vector2Int(id, id + 1);
            var idx = m_allocMap.FindIndex(r => RangeOverlap(r, range));
            if (idx < 0) return false;

            // Get reaction on split
            var insertLeft = true;
            var insertRight = true;
            
            var splitTarget = m_allocMap[id];
            if (splitTarget.x == range.x) insertLeft = false;
            if (splitTarget.y == range.y) insertRight = false;

            if (insertLeft || insertRight)
            {
                if (insertLeft)
                {
                    m_allocMap[idx] = new Vector2Int(splitTarget.x, range.x);
                }

                if (insertRight)
                {
                    m_allocMap.Insert(idx + 1, new Vector2Int(range.y, splitTarget.y));
                }
            }
            else
            {
                m_allocMap.RemoveAt(idx);
            }

            return true;
        }
        

        #endregion

        private struct Capture
        {
            public readonly ulong CaptureValidateNumber;

            public readonly int ResourceIndex;

            public object Context
            {
                get
                {
                    if (IsValid()) return _core[ResourceIndex].Context;
                    return _fallbackCore.Context;
                }
            }

            public string Label
            {
                get
                {
                    if (IsValid()) return _core[ResourceIndex].Label;
                    return _fallbackCore.Label;
                }
            }

            public bool TryResolveParent(out Capture capture, bool includeFallback = false)
            {
                if (IsValid())
                {
                    capture = new Capture(_core[ResourceIndex].ParentIndex);
                    if (capture.IsFallback()) return includeFallback;
                    return capture.CaptureValidateNumber == _core[ResourceIndex].ParentValidateNumber;
                }

                capture = default;
                return includeFallback;
            }

            public Capture(int idx)
            {
                if (idx >= _core.Count || idx <= 0)
                {
                    CaptureValidateNumber = default;
                    ResourceIndex = default;
                    return;
                }

                ResourceIndex = idx;
                CaptureValidateNumber = _core[idx].ValidateNumber;
            }

            public bool IsFallback()
            {
                return ResourceIndex == 0;
            }

            public bool IsValid()
            {
                var resIdx = ResourceIndex;
                if (resIdx == 0) return true;
                if (resIdx >= _core.Count || resIdx < 0) return false;
                return _core[resIdx].ParentValidateNumber == CaptureValidateNumber && IsIdUsed(resIdx);
            }
        }

        private struct Core
        {
            public ulong ValidateNumber;
            
            public ulong ParentValidateNumber;

            public int ParentIndex;

            public object Context;

            public string Label;
        }
        
        private static bool RequestCore(out Capture capture, string label, object context, int parentId)
        {
            var id = GetUnusedId();
            if (AllocId(id))
            {
                // Make sure there has valid space to allocate.
                while (_core.Count - 1 < id) _core.Add(default);
                
                var c = _core[id];
                
                // Set context of current Debug.
                c.Context = context;
                c.Label = label;
                
                // Check is given parent id is valid and fill result.
                c.ParentIndex = parentId != 0 && IsIdUsed(parentId) ? parentId : 0;
                c.ParentValidateNumber = c.ParentIndex == 0 ? 0 : _core[c.ParentIndex].ValidateNumber;

                _core[id] = c;
                capture = new Capture(id);
                return true;
            }

            capture = default;
            return false;
        }

        private static bool ReturnCore(Capture capture)
        {
            if (capture.IsFallback() || !capture.IsValid()) return false;
            if (!FreeId(capture.ResourceIndex)) return false;
            
            var core = _core[capture.ResourceIndex];
            
            core.ValidateNumber += 1;
            core.ParentValidateNumber = default;
            core.ParentIndex = default;
            core.Context = null;
            core.Label = kFallbackLabelName;
            
            _core[capture.ResourceIndex] = core;
            return true;
        }

        #endregion

        #region ExposeValue

        public bool IsFallback => m_capture.IsFallback();

        public string Label => m_capture.Label;

        public object Context => m_capture.Context;

        public Log? Parent
        {
            get
            {
                if (m_capture.TryResolveParent(out var cap, false))
                    return new Log(cap);

                return null;
            }
        }

        #endregion

        #region Lifecircle
        
        private readonly Capture m_capture;

        private Log(Capture capture)
        {
            m_capture = capture;
        }

        public Log(Log parent = default)
        {
            if (!RequestCore(out var capture, kNoLabelAndContextName, null, parent.m_capture.ResourceIndex))
            {
                m_capture = default;
                return;
            }

            m_capture = capture;
        }

        public Log(string label, Log parent = default)
        {
            if (string.IsNullOrWhiteSpace(label)) label = kNoLabelAndContextName;
            if (!RequestCore(out var capture, label, null, parent.m_capture.ResourceIndex))
            {
                m_capture = default;
                return;
            }

            m_capture = capture;
        }
        
        public Log(object context, Log parent = default)
        {
            var label = context == null ? kNoLabelAndContextName : context.GetType().Name;
            if (!RequestCore(out var capture, label, null, parent.m_capture.ResourceIndex))
            {
                m_capture = default;
                return;
            }

            m_capture = capture;
        }

        public Log(string label, object context, Log parent = default)
        {
            if (string.IsNullOrWhiteSpace(label)) label = context == null ? kNoLabelAndContextName : context.GetType().Name;
            if (!RequestCore(out var capture, label, null, parent.m_capture.ResourceIndex))
            {
                m_capture = default;
                return;
            }

            m_capture = capture;
        }

        public void Dispose()
        {
            if (!FreeId(m_capture.ResourceIndex) || !ReturnCore(m_capture)) throw new InvalidOperationException(
                "You can not trying to dispose an outdated or default AdvLogger!");
        }

        #endregion

        public void Debug(string text)
        {
        }
        
        public void Info(string text)
        {}
        
        public void Warning(string text)
        {}
        
        public void Error(string text)
        {}
        
        public void Error(Exception err)
        {}
    }
    
    public static class QLog
    {
        
        #region (string)

        public static void Debug(string text)
        {
            default(Log).Debug(text);
        }

        public static void Info(string text)
        {
            default(Log).Info(text);
        }

        public static void Warning(string text)
        {
            default(Log).Warning(text);
        }

        public static void Error(string text)
        {
            default(Log).Error(text);
        }

        #endregion

        #region (string, string)

        public static void Debug(string text, string label)
        {
            using var logger = new Log(label);
            logger.Debug(text);
        }
        
        public static void Info(string text, string label)
        {
            using var logger = new Log(label);
            logger.Info(text);
        }
        
        public static void Warning(string text, string label)
        {
            using var logger = new Log(label);
            logger.Warning(text);
        }
        
        public static void Error(string text, string label)
        {
            using var logger = new Log(label);
            logger.Error(text);
        }

        #endregion

        #region (string, object)

        public static void Debug(string text, object context)
        {
            using var logger = new Log(context);
            logger.Debug(text);
        }
        
        public static void Info(string text, object context)
        {
            using var logger = new Log(context);
            logger.Info(text);
        }
        
        public static void Warning(string text, object context)
        {
            using var logger = new Log(context);
            logger.Warning(text);
        }
    
        public static void Error(string text, object context)
        {
            using var logger = new Log(context);
            logger.Error(text);
        }

        #endregion

        #region (string, string, object)

        public static void Debug(string text, string label, object context)
        {
            using var logger = new Log(label, context);
            logger.Info(text);
        }
        
        public static void Info(string text, string label, object context)
        {
            using var logger = new Log(label, context);
            logger.Info(text);
        }
        
        public static void Warning(string text, string label, object context)
        {
            using var logger = new Log(label, context);
            logger.Warning(text);
        }
        
        public static void Error(string text, string label, object context)
        {
            using var logger = new Log(label, context);
            logger.Error(text);
        }

        #endregion
    }
}