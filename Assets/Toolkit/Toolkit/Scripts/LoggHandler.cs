using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ca2d.Toolkit
{
    public class LoggHandler : ILoggHandler
    {
        private readonly ILogHandler m_innerHandle;
        
        private readonly StringBuilder m_sb = new();

        private bool m_useLabel = false;

        private string m_labelText = default;
        
        #region PassThrough
        
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            LoggFormat((LoggType) logType, context, format, args);
        }

        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void LogException(Exception exception, Object context)
        {
            LoggFormat(LoggType.Exception, context, exception.ToString());
        }

        #endregion
        
        private void GenerateLabel(StringBuilder sb, LoggType type)
        {
            var logTypeText = type switch
            {
                LoggType.Assert => "ASSERT",
                LoggType.Exception => "EXCEPTION",
                LoggType.Error => "Error",
                LoggType.Warning => "Warning",
                LoggType.Log => "Info",
                _ => "Debug"
            };
            
            if (m_useLabel)
            {
                sb.AppendFormat("[{0:yyyy-M-d HH:mm:ss}] {1} ({2}) : ", DateTime.Now, logTypeText, m_labelText);
            }
            else
            {
                sb.AppendFormat("[{0:yyyy-M-d HH:mm:ss}] {1} : ", DateTime.Now, logTypeText);
            }
        }
        
        [HideInCallstack]
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void LoggFormat(LoggType logType, Object context, string format, params object[] args)
        {
            m_sb.Clear();
            
            // Write label to the front
            GenerateLabel(m_sb, logType);
            
            // Write format text after front
            m_sb.AppendFormat(format, args);

            // Redirect Debug to Log.
            LogType finalType;
            if (logType == LoggType.Debug) finalType = LogType.Log;
            else finalType = (LogType) logType;

            // Use inner handle to write.
            m_innerHandle.LogFormat(finalType, context, m_sb.ToString());
        }

        public ILogHandler InnerLogHandler => m_innerHandle;

        public void SetLabel(string label)
        {
            m_labelText = label;
            m_useLabel = label != null;
        }

        public void ClearLabel()
        {
            m_labelText = null;
            m_useLabel = false;
        }

        public LoggHandler(ILogHandler innerHandle)
        {
            m_innerHandle = innerHandle ?? throw new ArgumentNullException(nameof(innerHandle));
        }
    }
}