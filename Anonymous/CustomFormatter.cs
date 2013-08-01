using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class CustomFormatter : AtomicService<Func<string, object, IFormatProvider, string>>, ICustomFormatter
    {
        public CustomFormatter(Func<object,string> format):this((s,x,p)=>format(x)){}
        public CustomFormatter(Func<string,object,string> format):this((s,x,p)=>format(s,x)){}
        public CustomFormatter(Func<string, object, IFormatProvider, string> format)
            : base(format)
        {
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return Delegate(format, arg, formatProvider);
        }
    }
}