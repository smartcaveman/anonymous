namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;
    using System.Collections;

    public class StructuralComparable : AtomicService<Func<object, IComparer, int>>, IStructuralComparable
    {
        public StructuralComparable(AtomicService<Func<object, IComparer, int>> compareTo)
            : base(compareTo)
        {
        }

        public int CompareTo(object other, IComparer comparer)
        {
            return this.Delegate.Invoke(other, comparer);
        }
    }
}