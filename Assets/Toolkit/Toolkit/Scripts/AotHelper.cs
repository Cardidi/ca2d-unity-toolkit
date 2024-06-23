using System;

namespace Ca2d.Toolkit
{
    public static class AotHelper
    {
        private static DateTime alwayasFalseSource = DateTime.UtcNow;
        
        /// <summary>
        /// Utility to provide a must be false option for Ahead-of-time compiler.
        /// </summary>
        /// <returns>False</returns>
        public static bool AlwaysFalseProvider()
        {
            return alwayasFalseSource.Year < 0;
        }

        /// <summary>
        /// Create ahead-of-time info for compile while you can never successfully call given action at runtime.
        /// </summary>
        public static void Ensure(Action call)
        {
            if (AlwaysFalseProvider()) call?.Invoke();
        }
    }
}