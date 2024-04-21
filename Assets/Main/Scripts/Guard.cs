using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ca2d.Toolkit
{
    public static class Guard
    {
        /*  IgnoreException Utils  */
        
        #region IgnoreException(Action)

        public static void IgnoreException(Action exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }
        
        public static T IgnoreException<T>(Func<T> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async UniTask IgnoreException(Func<UniTask> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async UniTask<T> IgnoreException<T>(Func<UniTask<T>> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async Task IgnoreException(Func<Task> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async Task<T> IgnoreException<T>(Func<Task<T>> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async ValueTask IgnoreException(Func<ValueTask> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async ValueTask<T> IgnoreException<T>(Func<ValueTask<T>> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }

        #endregion
        
        #region IgnoreException(this Source, Action<Source::Type>)

        public static void IgnoreException<TSource>(TSource source, Action<TSource> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }
        
        public static T IgnoreException<TSource, T>(TSource source, Func<TSource, T> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async UniTask IgnoreException<TSource>(TSource source, Func<TSource, UniTask> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async UniTask<T> IgnoreException<TSource, T>(TSource source, Func<TSource, UniTask<T>> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async Task IgnoreException<TSource>(TSource source, Func<TSource, Task> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async Task<T> IgnoreException<TSource, T>(TSource source, Func<TSource, Task<T>> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async ValueTask IgnoreException<TSource>(this TSource source, Func<TSource, ValueTask> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async ValueTask<T> IgnoreException<TSource, T>(this TSource source, Func<TSource, ValueTask<T>> exec)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }

        #endregion
        
        #region IgnoreException(Action<P1::Type>, P1)

        public static void IgnoreException<TP1>(Action<TP1> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }
        
        public static T IgnoreException<TP1, T>(Func<TP1, T> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async UniTask IgnoreException<TP1>(Func<TP1, UniTask> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async UniTask<T> IgnoreException<TP1, T>(Func<TP1, UniTask<T>> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async Task IgnoreException<TP1>(Func<TP1, Task> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async Task<T> IgnoreException<TP1, T>(Func<TP1, Task<T>> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        public static async ValueTask IgnoreException<TP1>(Func<TP1, ValueTask> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        public static async ValueTask<T> IgnoreException<TP1, T>(Func<TP1, ValueTask<T>> exec, TP1 p1)
        {
            try
            {
                if (exec == null) throw new ArgumentNullException(
                    nameof(exec),
                    "Ignore Exception should run with executor!");
                
                return await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }

        #endregion

        /*   UnityObject Related   */
        
        #region RequireComponent

        public static bool ValidateOptional<T>(this Option<T> opt, out T component) where T : Component
        {
            if (opt.Enabled)
            {
                if (opt.Value == null)
                    throw new Exception();
                
                component = opt.Value;
                return true;
            }

            component = null;
            return false;
        }
        
        public static GameObject RequestComponent<T>(this GameObject go, out T component) where T : Component
        {
            if (!go.TryGetComponent<T>(out component))
            {
                component = go.AddComponent<T>();
            }

            return go;
        }
        
        public static GameObject RequestComponent<T, TSrc>(this TSrc source, out T component)
            where TSrc : Component
            where T : Component
        {
            var go = source.gameObject;
            if (!go.TryGetComponent<T>(out component))
            {
                component = go.AddComponent<T>();
            }

            return go;
        }
        
        public static GameObject RequireComponent<T>(this GameObject go, out T component)
        {
            if (!go.TryGetComponent<T>(out component))
            {
                throw new Exception();
            }

            return go;
        }
        
        public static TSrc RequireComponent<T, TSrc>(this TSrc source, out T component) where TSrc : Component
        {
            if (!source.TryGetComponent<T>(out component))
            {
                throw new Exception();
            }

            return source;
        }
        
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (!go.TryGetComponent<T>(out var comp))
            {
                comp = go.AddComponent<T>();
            }

            return comp;
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            var go = component.gameObject;
            if (!go.TryGetComponent<T>(out var comp))
            {
                comp = go.AddComponent<T>();
            }

            return comp;
        }
        
        #endregion
    }
}   