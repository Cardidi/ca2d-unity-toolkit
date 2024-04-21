namespace Ca2d.Toolkit
{
    public class Ref<T> where T : struct
    {
        private T m_value;

        public Ref(T mValue)
        {
            m_value = mValue;
        }

        public T Value
        {
            get => m_value;
            set => m_value = value;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public static implicit operator T(Ref<T> wrapper)
        {
            return wrapper.Value;
        }

        public static implicit operator Ref<T>(T value)
        {
            return new Ref<T>(value);
        }
    }
}