namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class Comparable<T> : AtomicService<Func<T, int>>, IComparable<T>
    {
        public Comparable(Func<T, int> compareTo)
            : base(compareTo)
        {
        }

        public int CompareTo(T other)
        {
            return this.Delegate(other);
        }
    }
}