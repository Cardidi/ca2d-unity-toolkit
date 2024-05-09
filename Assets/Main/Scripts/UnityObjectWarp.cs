using System;
using UnityEngine;

namespace Ca2d.Toolkit
{
    /// <summary>
    /// An utility which allows you to reference an <see cref="UnityEngine.Object"/> via it's interface.
    /// </summary>
    /// <typeparam name="T">The type of class which you are willing to reference as.</typeparam>
    [Serializable]
    public struct UnityObjectWarp<T> where T : class
    {
        [SerializeField] private UnityEngine.Object m_referencedObject;

        /// <summary>
        /// Is this warp reference to a valid target?
        /// </summary>
        public bool Is => m_referencedObject is T;
        
        /// <summary>
        /// Trying to get the actual object of this reference.
        /// </summary>
        public T As => m_referencedObject as T;
        
        /// <summary>
        /// Is this warp reference to a valid target? If this is valid, give the casting result.
        /// </summary>
        public bool IsAs(out T val)
        {
            if (m_referencedObject is T referenced)
            {
                val = referenced;
                return true;
            }
            
            val = null;
            return false;
        }
    }
}