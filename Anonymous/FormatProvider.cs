using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class FormatProvider : AtomicService<Func<Type, object>>, IFormatProvider
    {
        public FormatProvider(Func<Type, object> getFormat)
            : base(getFormat)
        {
        }

        public object GetFormat(Type formatType)
        {
            return Delegate(formatType);
        }
    }
}