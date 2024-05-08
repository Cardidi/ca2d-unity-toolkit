using System;

namespace Ca2d.Toolkit
{
    public class LoseRequirementException : Exception
    {
        public Type RequiredType { get; }

        public LoseRequirementException(Type type) : base($"Required type {type?.FullName} was lost when resolving.")
        {
            RequiredType = type;
        }
        
        public LoseRequirementException(Object obj) : base($"Required type {obj?.GetType().FullName} was lost when resolving.")
        {
            RequiredType = obj?.GetType();
        }
        
        
        public LoseRequirementException(ValueType obj) : base($"Required type {obj.GetType().FullName} was lost when resolving.")
        {
            RequiredType = obj.GetType();
        }
    }
}