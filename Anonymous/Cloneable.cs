using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Cloneable : AtomicService<Func<object>>, ICloneable
    {
        public Cloneable(Func<object> clone)
            : base(clone)
        {
        }

        public object Clone()
        {
            return Delegate();
        }
    }
}