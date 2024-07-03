#if ENABLE_LOGG
using System;
using System.Diagnostics;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ca2d.Toolkit
{
    /// <summary>
    /// Alternatives to the default Unity Logger.
    /// </summary>
    public struct Logg
    {
        #region BackendConfig

        // ReSharper disable once MemberCanBePrivate.Global
        public const string kDefaultLoggerLevelSplit = "::";

        private static ILoggHandler _backend;

        /// <summary>
        /// Which backend will be used for logger?
        /// </summary>
        public static ILoggHandler Backend
        {
            get => _backend;
            set => _backend = value ?? throw new NullReferenceException();
        }

        /// <summary>
        /// Logger level name spliter sign.
        /// </summary>
        public static string LoggerLevelSplit { get; set; } = kDefaultLoggerLevelSplit;

        #endregion

        #region Constructor

        private static readonly StringBuilder kStringBuilder = new();

        private readonly bool m_useLabel;

        private readonly string m_label;

        /// <summary>
        /// Create a logger with first-level namespace
        /// </summary>
        /// <param name="ns">Namespace</param>
        public Logg(string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
            {
                m_useLabel = false;
                m_label = null;
            }
            else
            {
                m_useLabel = true;
                m_label = ns.Trim();
            }
        }

        /// <summary>
        /// Create a copy of source logger. 
        /// </summary>
        /// <param name="source">Source logger.</param>
        public Logg(Logg source)
        {
            if (source.m_useLabel)
            {
                m_useLabel = true;
                m_label = source.m_label;
            }
            else
            {
                m_useLabel = false;
                m_label = null;
            }
        }
        
        /// <summary>
        /// Create a nested logger with given namespace.
        /// </summary>
        /// <param name="source">Nest source.</param>
        /// <param name="ns">Namespace</param>
        public Logg(Logg source, string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
            {
                if (source.m_useLabel)
                {
                    m_useLabel = true;
                    m_label = source.m_label;
                }
                else
                {
                    m_useLabel = false;
                    m_label = null;
                }
            }
            else
            {
                m_useLabel = true;
                if (source.m_useLabel)
                {
                    kStringBuilder.Clear();
                    m_label = kStringBuilder.AppendJoin(LoggerLevelSplit, source.m_label, ns.Trim()).ToString();
                }
                else
                {
                    m_label = ns.Trim();
                }
            }
            
        }

        #endregion

        #region EnvOverrider

        private static bool _init = false;
        
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void LoggInitEditor()
        {
            LoggInit();
        }
#else
        [RuntimeInitializeOnLoadMethod]
        private static void RuntimeLoggInit()
        {
            LoggInit();
        }
#endif

        private static void LoggInit()
        {
            if (!_init)
            {
                var prevLogHandler = UnityEngine.Debug.unityLogger.logHandler;
                
                if (prevLogHandler is ILoggHandler)
                {
                    prevLogHandler.LogFormat(
                        LogType.Warning,
                        null,
                        "Logg seems to be inited before initialize?");
                }
                else
                {
                    var l = new LoggHandler(prevLogHandler);
                    _backend = l;
                    UnityEngine.Debug.unityLogger.logHandler = l;
                    prevLogHandler.LogFormat(LogType.Log, null, "##### Debug.unityLogger was taken over by Logg #####");
                }
                
                _init = true;
            }
        }

        #endregion

        #region LogMethods

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Format(LoggType type, Object context, string format, params object[] args)
        {
            if (_backend != null)
            {
                if (m_useLabel) _backend.SetLabel(m_label);
                _backend.LoggFormat(type, context, format, args);
                _backend.ClearLabel();
            }
            else
            {
                // Use fallback logger if Logg is not ready.
                UnityEngine.Debug.LogFormat((LogType) type, LogOption.NoStacktrace, context, format, args);
            }
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Debug(string content, Object context = null)
        {
            Format(LoggType.Debug, context, content);
        }
        
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Info(string content, Object context = null)
        {
            Format(LoggType.Log, context, content);
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Warning(string content, Object context = null)
        {
            Format(LoggType.Warning, context, content);
        }
        
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Error(string content, Object context = null)
        {
            Format(LoggType.Error, context, content);
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Error(Exception err, Object context = null)
        {
            Format(LoggType.Exception, context, err.ToString());
        }
        
        #endregion
        
    }
    
    /// <summary>
    /// Quick log API for <see cref="Logg"/>.
    /// </summary>
    public static class DebugLogg
    {
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public static void Format(LoggType type, Object context, string format, params object[] args)
        {
            default(Logg).Format(type, context, format, args);
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public static void Debug(string content, Object context = null)
        {
            default(Logg).Debug(content, context);
        }
        
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public static void Info(string content, Object context = null)
        {
            default(Logg).Info(content, context);
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public static void Warning(string content, Object context = null)
        {
            default(Logg).Warning(content, context);
        }
        
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public static void Error(string content, Object context = null)
        {
            default(Logg).Error(content, context);
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public static void Error(Exception err, Object context = null)
        {
            default(Logg).Error(err, context);
        }
    }
}
#endif