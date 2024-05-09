using UnityEngine;

namespace Ca2d.Toolkit
{
    public enum LoggType
    {
        /// <summary>
        ///   <para>LogType used for Errors.</para>
        /// </summary>
        Error,
        /// <summary>
        ///   <para>LogType used for Asserts. (These could also indicate an error inside Unity itself.)</para>
        /// </summary>
        Assert,
        /// <summary>
        ///   <para>LogType used for Warnings.</para>
        /// </summary>
        Warning,
        /// <summary>
        ///   <para>LogType used for regular log messages.</para>
        /// </summary>
        Log,
        /// <summary>
        ///   <para>LogType used for Exceptions.</para>
        /// </summary>
        Exception,
        /// <summary>
        ///   <para>LogType used for Developers on testing.</para>
        /// </summary>
        Debug
    }
    
    public interface ILoggHandler : ILogHandler
    {
        public ILogHandler InnerLogHandler { get; }

        public void LoggFormat(LoggType logType, Object context, string format, params object[] args);
        
        public void SetLabel(string label);

        public void ClearLabel();
    }
}