using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ca2d.Toolkit
{

    public delegate void ResLockStateChanged(ResLock resLock, uint notionId, bool notionSelfState);
    
    public class ResLock
    {
        private struct ResourceNotionCore : IDisposable
        {

            #region Allocator

            private static SortedSet<uint> m_allocateMap = new();

            private static uint AllocateId()
            {
                // Id should start at 1 to avoid 0 as default.
                uint id = 1;
                foreach (var u in m_allocateMap)
                {
                    if (id < u) break;
                    id = u + 1;
                }

                m_allocateMap.Add(id);
                return id;
            }
            
            private static void FreeId(uint id)
            {
                m_allocateMap.Remove(id);
            }

            #endregion

            #region Notification

            
            private static bool _LockStateNotifyGuard = false;

            private static void NotifyLoopDetect()
            {
                if (_LockStateNotifyGuard) throw new InvalidOperationException();
            }

            private static void Notify(ResLock target, ref ResourceNotionCore core)
            {
                _LockStateNotifyGuard = true;
            
                try
                {
                    _LockStateEvents?.Invoke(target, core.Id, core.Target.m_resNotions.Contains(core.Id));
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

                _LockStateNotifyGuard = false;
            }


            #endregion
            
            public readonly uint Id;

            public readonly ResLock Target;

            public ResourceNotionCore(ResLock target)
            {
                Target = target;
                Id = AllocateId();
#if DEBUG
                Debug.LogFormat("Resource Notion '{0}' was allocated for {1}", Id, target.FriendlyName);
#endif
            }

            public bool SetState(bool enabled)
            {
                NotifyLoopDetect();
                
                var result = enabled ? Target.m_resNotions.Add(Id) : Target.m_resNotions.Remove(Id);
                Notify(Target, ref this);
                
                return result;
            }

            public void Dispose()
            {
                FreeId(Id);
#if DEBUG
                Debug.LogFormat("Resource Notion '{0}' was released from {1}", Id, Target.FriendlyName);
#endif
            }
        }
        
        public struct LiteResourceNotion : IDisposable
        {
            private ResourceNotionCore m_core;

            internal LiteResourceNotion(ResLock parent)
            {
                m_core = new ResourceNotionCore(parent);
                m_core.SetState(true);
            }
            
            public void Dispose()
            {
                if (m_core.Target == null)
                    throw new InvalidOperationException("Can not dispose a disposed Resource Notion!");

                m_core.SetState(false);
                m_core.Dispose();
                m_core = default;
            }
        }
        
        
        public class ResourceNotion : IDisposable
        {
            private ResourceNotionCore m_core;

            /// <summary>
            /// Event when locking state changed.
            /// </summary>
            public event Action<bool> OnStateChanged; 
            
            /// <summary>
            /// The locking state of this notion.
            /// </summary>
            public bool LockStateSelf { get; private set; }
            
            /// <summary>
            /// The parent of this notion.
            /// </summary>
            public ResLock Parent => m_core.Target;

            internal ResourceNotion(ResLock parent)
            {
                m_core = new ResourceNotionCore(parent);
                LockStateSelf = false;
                _LockStateEvents += NotifyListener;
            }

            /// <summary>
            /// Lock this notion
            /// </summary>
            public bool Lock()
            {
                Guard();
                if (m_core.SetState(true))
                {
                    LockStateSelf = true;
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Unlock this notion.
            /// </summary>
            public bool Unlock()
            {
                Guard();
                if (m_core.SetState(false))
                {
                    LockStateSelf = false;
                    return true;
                }

                return false;
            }
            
            public void Dispose()
            {
                if (m_core.Target == null)
                    throw new InvalidOperationException("Can not dispose a disposed Resource Notion!");

                m_core.SetState(false);
                _LockStateEvents -= NotifyListener;
                LockStateSelf = false;
                m_core.Dispose();
                m_core = default;
            }
            
            private void NotifyListener(ResLock target, uint id, bool selfState)
            {
                if (target == Parent)
                {
                    OnStateChanged?.Invoke(target.LockState);
                }
            }

            private void Guard()
            {
                if (m_core.Target == null)
                    throw new InvalidOperationException("Can not operate a disposed Resource Notion!");
            }
        }

        private static event ResLockStateChanged _LockStateEvents
#if DEBUG
                = (locker, id, selfState) =>
                {
                    var name = locker.FriendlyName;
                    var state = locker.LockState;

                    using (StringBuilderPool.Get(out var sb))
                    {
                        sb.AppendFormat(
                            "Resource Notion '{0}' changes to {1} made Locker '{2}' state changed to {3} ",
                            id,
                            selfState ? "True" : "False",
                            name,
                            state ? "True" : "False");
                        
                        sb.AppendFormat("(limit {0}:, used: {1})", locker.m_resCount, locker.m_resNotions.Count);

                        Debug.Log(sb);
                    }
                }
#endif
            ;

        private uint m_resCount;

        private HashSet<uint> m_resNotions = new ();

        public ResLock(uint resCount = 0, string friendlyName = default)
        {
            m_resCount = resCount;
            FriendlyName = friendlyName;
        }

        /// <summary>
        /// User friendly name for ResLock
        /// </summary>
        public string FriendlyName { get; }

        /// <summary>
        /// State of this lock itself
        /// </summary>
        public bool LockState => m_resNotions.Count > m_resCount;

        /// <summary>
        /// Do locking on this locker
        /// </summary>
        /// <param name="requireLockStateIsFree">Should require this lock is free</param>
        /// <returns>Locker Notion can be used in using block</returns>
        /// <exception cref="InvalidOperationException">Lock can not being operated.</exception>
        public LiteResourceNotion Lock(bool requireLockStateIsFree = true)
        {
            if (requireLockStateIsFree && LockState)
            {
                if (string.IsNullOrWhiteSpace(FriendlyName)) throw new InvalidOperationException(
                    "Can not lock Resource Lock due to free state is required.");
                else throw new InvalidOperationException(
                    $"Can not lock Resource Lock '{FriendlyName}' due to free state is required.");
            }

            return new LiteResourceNotion(this);
        }

        /// <summary>
        /// 获取一个锁注解
        /// </summary>
        /// <param name="initialLock">该注解的起始锁定状态</param>
        public ResourceNotion GetNotion(bool initialLock = false)
        {
            var n = new ResourceNotion(this);
            if (initialLock) n.Lock();

            return n;
        }
    }
}