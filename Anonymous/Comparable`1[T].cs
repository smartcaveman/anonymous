using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Comparable<T> : AtomicService<Func<T,int>>, IComparable<T>
    {
        protected Comparable(Func<T, int> component) : base(component)
        {
        }

        public int CompareTo(T other)
        {
            return Delegate(other);
        }
    }
}