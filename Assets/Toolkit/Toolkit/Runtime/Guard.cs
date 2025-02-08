using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ca2d.Toolkit
{
    public static class Guard
    {
        /*  IgnoreException Utils  */

        private static void IgnoreNoTarget()
        {
#if UNITY_EDITOR
            Debug.LogError("Ignore Exception should run with executor!");
#endif
        }
        
        #region IgnoreException(Action)

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        public static void IgnoreException(Action exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }
            
            try
            {
                exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        public static T IgnoreException<T>(Func<T> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }
            
            try
            {
                return exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        public static async UniTask IgnoreExceptionAsync(Func<UniTask> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }
            
            try
            {
                await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        public static async UniTask<T> IgnoreExceptionAsync<T>(Func<UniTask<T>> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }
            
            try
            {
                return await exec.Invoke();
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        #endregion
        
        #region IgnoreException(this Source, Action<Source>)

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="source">The source of this operation</param>
        /// <param name="exec">Executor of source executor</param>
        public static void IgnoreException<TSource>(this TSource source, Action<TSource> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }
                
            try
            {
                exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="source">The source of this operation</param>
        /// <param name="exec">Executor of source executor</param>
        public static T IgnoreException<TSource, T>(this TSource source, Func<TSource, T> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }
            
            
            try
            {
                return exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="source">The source of this operation</param>
        /// <param name="exec">Executor of source executor</param>
        public static async UniTask IgnoreExceptionAsync<TSource>(this TSource source, Func<TSource, UniTask> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }
                
            try
            {
                await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="source">The source of this operation</param>
        /// <param name="exec">Executor of source executor</param>
        public static async UniTask<T> IgnoreExceptionAsync<TSource, T>(this TSource source, Func<TSource, UniTask<T>> exec)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }
            
            try
            {
                return await exec.Invoke(source);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }

        #endregion
        
        #region IgnoreException(Action<P1>, P1)

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        public static void IgnoreException<TP1>(Action<TP1> exec, TP1 p1)
        {
            
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }
            
            try
            {
                exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        public static T IgnoreException<TP1, T>(Func<TP1, T> exec, TP1 p1)
        {
            
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }
            
            try
            {
                return exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        public static async UniTask IgnoreExceptionAsync<TP1>(Func<TP1, UniTask> exec, TP1 p1)
        {
            
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }
            
            try
            {
                await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        public static async UniTask<T> IgnoreExceptionAsync<TP1, T>(Func<TP1, UniTask<T>> exec, TP1 p1)
        {
            
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }
            
            try
            {
                return await exec.Invoke(p1);
            }
            catch (Exception e)
            { Debug.LogError(e); }

            return default(T);
        }

        #endregion

        #region IgnoreException(Action<P1, P2>, P1, P2)

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        public static void IgnoreException<TP1, TP2>(Action<TP1, TP2> exec, TP1 p1, TP2 p2)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                exec.Invoke(p1, p2);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        public static T IgnoreException<TP1, TP2, T>(Func<TP1, TP2, T> exec, TP1 p1, TP2 p2)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return exec.Invoke(p1, p2);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        public static async UniTask IgnoreExceptionAsync<TP1, TP2>(Func<TP1, TP2, UniTask> exec, TP1 p1, TP2 p2)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                await exec.Invoke(p1, p2);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }


        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        public static async UniTask<T> IgnoreExceptionAsync<TP1, TP2, T>(
            Func<TP1, TP2, UniTask<T>> exec, TP1 p1, TP2 p2)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return await exec.Invoke(p1, p2);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }


        #endregion
        
        #region IgnoreException(Action<P1, P2, P3>, P1, P2, P3)
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        public static void IgnoreException<TP1, TP2, TP3>(
            Action<TP1, TP2, TP3> exec, 
            TP1 p1, TP2 p2, TP3 p3)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                exec.Invoke(p1, p2, p3);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        public static T IgnoreException<TP1, TP2, TP3, T>(
            Func<TP1, TP2, TP3, T> exec, 
            TP1 p1, TP2 p2, TP3 p3)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return exec.Invoke(p1, p2, p3);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        public static async UniTask IgnoreExceptionAsync<TP1, TP2, TP3>(
            Func<TP1, TP2, TP3, UniTask> exec, 
            TP1 p1, TP2 p2, TP3 p3)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                await exec.Invoke(p1, p2, p3);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        public static async UniTask<T> IgnoreExceptionAsync<TP1, TP2, TP3, T>(
            Func<TP1, TP2, TP3, UniTask<T>> exec,
            TP1 p1, TP2 p2, TP3 p3)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return await exec.Invoke(p1, p2, p3);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }

        
        #endregion
        
        #region IgnoreException(Action<P1, P2, P3, P4>, P1, P2, P3, P4)

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        public static void IgnoreException<TP1, TP2, TP3, TP4>(
            Action<TP1, TP2, TP3, TP4> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                exec.Invoke(p1, p2, p3, p4);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        public static T IgnoreException<TP1, TP2, TP3, TP4, T>(
            Func<TP1, TP2, TP3, TP4, T> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return exec.Invoke(p1, p2, p3, p4);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        public static async UniTask IgnoreExceptionAsync<TP1, TP2, TP3, TP4>(
            Func<TP1, TP2, TP3, TP4, UniTask> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                await exec.Invoke(p1, p2, p3, p4);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        public static async UniTask<T> IgnoreExceptionAsync<TP1, TP2, TP3, TP4, T>(
            Func<TP1, TP2, TP3, TP4, UniTask<T>> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return await exec.Invoke(p1, p2, p3, p4);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }

        
        #endregion
        
        
        #region IgnoreException(Action<P1, P2, P3, P4, P5>, P1, P2, P3, P4, P5)
        
        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        /// <param name="p5">Fifth parameter of this method</param>
        public static void IgnoreException<TP1, TP2, TP3, TP4, TP5>(
            Action<TP1, TP2, TP3, TP4, TP5> exec, 
            TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                exec.Invoke(p1, p2, p3, p4, p5);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        /// <param name="p5">Fifth parameter of this method</param>
        public static T IgnoreException<TP1, TP2, TP3, TP4, TP5, T>(
            Func<TP1, TP2, TP3, TP4, TP5, T> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return exec.Invoke(p1, p2, p3, p4, p5);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return default(T);
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        /// <param name="p5">Fifth parameter of this method</param>
        public static async UniTask IgnoreExceptionAsync<TP1, TP2, TP3, TP4, TP5>(
            Func<TP1, TP2, TP3, TP4, TP5, UniTask> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return;
            }

            try
            {
                await exec.Invoke(p1, p2, p3, p4, p5);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Ignore <see cref="Exception"/>s raised with this method and log them into console directly.
        /// </summary>
        /// <param name="exec">Executor of method</param>
        /// <param name="p1">First parameter os this method</param>
        /// <param name="p2">Second parameter of this method</param>
        /// <param name="p3">Third parameter of this method</param>
        /// <param name="p4">Fourth parameter of this method</param>
        /// <param name="p5">Fifth parameter of this method</param>
        public static async UniTask<T> IgnoreExceptionAsync<TP1, TP2, TP3, TP4, TP5, T>(
            Func<TP1, TP2, TP3, TP4, TP5, UniTask<T>> exec,
            TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5)
        {
            if (exec == null)
            {
                IgnoreNoTarget();
                return default(T);
            }

            try
            {
                return await exec.Invoke(p1, p2, p3, p4, p5);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

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
                    throw new LoseRequirementException(typeof(T));
                
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
                throw new LoseRequirementException(typeof(T));
            }

            return go;
        }
        
        public static TSrc RequireComponent<T, TSrc>(this TSrc source, out T component) where TSrc : Component
        {
            if (!source.TryGetComponent<T>(out component))
            {
                throw new LoseRequirementException(typeof(T));
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