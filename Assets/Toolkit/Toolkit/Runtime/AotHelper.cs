using System;
using System.Reflection;

namespace Ca2d.Toolkit
{
    public static class AotHelper
    {
        private static readonly DateTime AlwaysFalseSource = DateTime.UtcNow;
        
        /// <summary>
        /// Utility to provide a must be false option for Ahead-of-time compiler.
        /// </summary>
        /// <returns>False</returns>
        public static bool False()
        {
            return AlwaysFalseSource.Year < 0;
        }

        /// <summary>
        /// Create ahead-of-time info for compile while you can never successfully call given action at runtime.
        /// </summary>
        public static void Ensure(Action call)
        {
            if (False()) call?.Invoke();
        }

        public static void Ensure<T>()
        {
            if (False()) typeof(T).GetTypeInfo();
        }
    }
}