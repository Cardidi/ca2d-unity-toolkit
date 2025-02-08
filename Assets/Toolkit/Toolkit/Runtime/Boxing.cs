using System;
using UnityEngine;

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
            m_value = source.m_value;
        }
        
        /// <summary>
        /// Create a copy of given value.
        /// </summary>
        public Boxing(T value)
        {
            m_value = value;
        }
        
        /// <summary>
        /// Create an empty boxing.
        /// </summary>
        public Boxing()
        {}

        /// <summary>
        /// Access to the boxing value directly in reference mode.
        /// </summary>
        public ref T Ref => ref m_value;

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

        public static explicit operator T(Boxing<T> wrapper)
        {
            return wrapper.m_value;
        }

        public static implicit operator Boxing<T>(T value)
        {
            return new Boxing<T>(value);
        }
    }
}