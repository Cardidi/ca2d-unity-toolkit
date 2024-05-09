using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Ca2d.Toolkit
{

    public static class AsDisposableExtension
    {
        /// <summary>
        /// Convert an <see cref="System.Action"/> to a IDisposable callback.
        /// </summary>
        public static AsDisposable AsDisposable(Action callback)
        {
            return new AsDisposable(callback);
        }
        
        /// <summary>
        /// Convert an <see cref="System.Action"/> to a IDisposable callback.
        /// </summary>
        public static AsAsyncDisposable AsAsyncDisposable(Func<UniTask> callback)
        {
            return new AsAsyncDisposable(callback);
        }
        
        /// <summary>
        /// Convert an <see cref="System.Action"/> to a IDisposable callback in chain style.
        /// </summary>
        public static AsDisposable<T> AsDisposable<T>(this T target, Action<T> callback)
        {
            return new AsDisposable<T>(callback, target);
        }
        
        
        /// <summary>
        /// Convert an <see cref="System.Action"/> to a IDisposable callback in chain style.
        /// </summary>
        public static AsAsyncDisposable<T> AsAsyncDisposable<T>(this T target, Func<T, UniTask> callback)
        {
            return new AsAsyncDisposable<T>(callback, target);
        }
    }
    
    /// <summary>
    /// An operation container for <see cref="IDisposable"/>.
    /// </summary>
    public readonly struct AsDisposable : IDisposable
    {
        private readonly Action m_callback;
        
        public AsDisposable(Action callback)
        {
            m_callback = callback;
        }

        public static implicit operator AsDisposable(Action callback)
        {
            return new AsDisposable(callback);
        }
        
        public void Dispose()
        {
            Guard.IgnoreException(m_callback);
        }
    }
    
    /// <summary>
    /// An operation container for <see cref="IDisposable"/>.
    /// </summary>
    /// <typeparam name="TParam">Object select source.</typeparam>
    public readonly struct AsDisposable<TParam> : IDisposable
    {
        private readonly Action<TParam> m_callback;

        private readonly TParam m_param;
        
        public AsDisposable(TParam param, Action<TParam> callback)
        {
            m_param = param;
            m_callback = callback;
        }
        
        public AsDisposable(Action<TParam> callback, TParam param)
        {
            m_param = param;
            m_callback = callback;
        }
        
        public void Dispose()
        {
            Guard.IgnoreException(m_callback, m_param);
        }
    }

    /// <summary>
    /// An operation container for <see cref="IDisposable"/>.
    /// </summary>
    public readonly struct AsAsyncDisposable : IAsyncDisposable
    {
        private readonly Func<UniTask> m_callback;
        
        public AsAsyncDisposable(Func<UniTask> callback)
        {
            m_callback = callback;
        }

        public static implicit operator AsAsyncDisposable(Func<UniTask> callback)
        {
            return new AsAsyncDisposable(callback);
        }
        
        public async ValueTask DisposeAsync()
        {
            if (m_callback == null) return;
            await Guard.IgnoreException(m_callback);
        }
    }
    
    /// <summary>
    /// An operation container for <see cref="IDisposable"/>.
    /// </summary>
    /// <typeparam name="TParam">Object select source.</typeparam>
    public readonly struct AsAsyncDisposable<TParam> : IAsyncDisposable
    {
        private readonly Func<TParam, UniTask> m_callback;

        private readonly TParam m_param;
        
        public AsAsyncDisposable(TParam param, Func<TParam, UniTask> callback)
        {
            m_param = param;
            m_callback = callback;
        }
        
        public AsAsyncDisposable(Func<TParam, UniTask> callback, TParam param)
        {
            m_param = param;
            m_callback = callback;
        }
        
        public async ValueTask DisposeAsync()
        {
            if (m_callback == null) return;
            await Guard.IgnoreException(m_callback, m_param);
        }
    }
}