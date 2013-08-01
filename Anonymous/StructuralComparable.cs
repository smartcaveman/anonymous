using System;
using System.Collections;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class StructuralComparable : AtomicService<Func<object, IComparer, int>>, IStructuralComparable
    {
        public StructuralComparable(AtomicService<Func<object, IComparer, int>> compareTo)
            : base(compareTo)
        {
        }

        public int CompareTo(object other, IComparer comparer)
        {
            return Delegate.Invoke(other, comparer);
        }
    }
}