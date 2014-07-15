namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;
    using System.Collections.Generic;

    public class Comparer<T> : AtomicService<Comparison<T>>, IComparer<T>
    {
        public Comparer(Comparison<T> compare)
            : base(compare)
        {
        }

        public int Compare(T x, T y)
        {
            return this.Delegate.Invoke(x, y);
        }

        public static implicit operator Comparer<T>(Comparison<T> comparison)
        {
            return comparison == null ? null : new Comparer<T>(comparison);
        }

        public static implicit operator Comparer<T>(Func<T, T, int> comparison)
        {
            return comparison == null ? null : new Comparer<T>(comparison.Invoke);
        }

        public static implicit operator Func<T, T, int>(Comparer<T> comparer)
        {
            return ReferenceEquals(comparer, null) ? default(Func<T, T, int>) : comparer.Compare;
        }
    }
}