namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class Cloneable : AtomicService<Func<object>>, ICloneable
    {
        public Cloneable(Func<object> clone)
            : base(clone)
        {
        }

        public object Clone()
        {
            return this.Delegate();
        }
    }
}