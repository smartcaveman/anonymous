using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class FormatProvider : AtomicService<Func<Type, object>>, IFormatProvider
    {
        protected FormatProvider(Func<Type, object> component) : base(component)
        {
        }

        public object GetFormat(Type formatType)
        {
            return Delegate(formatType);
        }
    }
}