using System;
using UnityEngine.Assertions;

namespace Ca2d.Toolkit
{
    /// <summary>
    /// An utility structure which helps you to create an optional field in Unity Editor or a <see cref="Nullable"/>
    /// alternative.
    /// </summary>
    /// <typeparam name="T">The type which will be optional.</typeparam>
    [Serializable]
    public struct Option<T>
    {
        public bool Enabled;

        public T Value;

        public T ValueOrDefault()
        {
            return Enabled ? Value : default;
        }

        public T ValueOrDefault(T def)
        {
            return Enabled ? Value : def;
        }

        public T ValueOrDefault(Func<T> def)
        {
            Assert.IsNotNull(def);
            return Enabled ? Value : def();
        }
        
        public T ValueOrDefault<TSelector>(TSelector src, Func<TSelector, T> def)
        {
            Assert.IsNotNull(def);
            return Enabled ? Value : def(src);
        }

        public Option(T value)
        {
            Enabled = true;
            Value = value;
        }
        
        public static implicit operator T(Option<T> wrapper)
        {
            return wrapper.Value;
        }

        public static implicit operator Option<T>(T value)
        {
            return new Option<T>(value);
        }
    }
}