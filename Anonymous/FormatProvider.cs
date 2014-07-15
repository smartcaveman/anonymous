namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class FormatProvider : AtomicService<Func<Type, object>>, IFormatProvider
    {
        public FormatProvider(Func<Type, object> getFormat)
            : base(getFormat)
        {
        }

        public object GetFormat(Type formatType)
        {
            return this.Delegate(formatType);
        }
    }
}