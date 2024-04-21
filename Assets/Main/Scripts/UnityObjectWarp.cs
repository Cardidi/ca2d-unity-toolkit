using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ca2d.Toolkit
{
    [Serializable]
    public struct UnityObjectWarp<T> where T : class
    {
        [SerializeField] private Object m_referencedObject;

        public bool Valid => m_referencedObject is T;
        
        public T Object => m_referencedObject as T;
        
        public static implicit operator T(UnityObjectWarp<T> wrapper)
        {
            return wrapper.Object;
        }
    }
}