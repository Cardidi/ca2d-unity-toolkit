using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ca2d.Toolkit
{
    /// <summary>
    /// An utility which gives a type safe boxing.
    /// </summary>
    /// <typeparam name="T">The value type which will being boxing.</typeparam>
    [Serializable]
    public class Boxing<T> where T : struct
    {
        [SerializeField] private T m_value;

        /// <summary>
        /// Create a copy of given boxing source.
        /// </summary>
        public Boxing(Boxing<T> source)
        {
            m_value = source.Unbox;
        }
        
        /// <summary>
        /// Create a copy of given value.
        /// </summary>
        public Boxing(T value)
        {
            m_value = value;
        }
        
        /// <summary>
        /// Create a empty boxing.
        /// </summary>
        public Boxing()
        {}

        /// <summary>
        /// Get a copy of boxing value.
        /// </summary>
        public T Unbox
        {
            get => m_value;
            set => m_value = value;
        }

        /// <summary>
        /// Access to the boxing value directly.
        /// </summary>
        public ref T Direct => ref m_value;

        /// <summary>
        /// Clean this boxing container to the default value of boxing target.
        /// </summary>
        public void Empty()
        {
            m_value = default;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public static implicit operator T(Boxing<T> wrapper)
        {
            return wrapper.Unbox;
        }

        public static implicit operator Boxing<T>(T value)
        {
            return new Boxing<T>(value);
        }
    }
}