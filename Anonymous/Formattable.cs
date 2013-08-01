using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Formattable :  AtomicService<Func<string, IFormatProvider, string>>, IFormattable
    {
        public Formattable(Func<string> toString):this((s,p)=>toString()){}
        public Formattable(Func<IFormatProvider,string> toString):this((s,p)=>toString(p)){}
        public Formattable(Func<string,string> toString):this((s,p) => toString(s)){}
        public Formattable(Func<string, IFormatProvider, string> toString)
            : base(toString)
        {
        }

        public string ToString(string format = null, IFormatProvider formatProvider = null)
        {
            return Delegate(format, formatProvider);
        }
    }
}