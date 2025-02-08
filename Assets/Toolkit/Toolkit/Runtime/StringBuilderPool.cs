using System.Text;
using UnityEngine.Pool;

namespace Ca2d.Toolkit
{
    public static class StringBuilderPool
    {
        private static readonly ObjectPool<StringBuilder> s_Pool = new (
            () => new StringBuilder(), 
            sb => sb.Clear());
        
        public static StringBuilder Get() => s_Pool.Get();

        public static PooledObject<StringBuilder> Get(out StringBuilder value) => s_Pool.Get(out value);

        public static void Release(StringBuilder toRelease) => s_Pool.Release(toRelease);
    }
}