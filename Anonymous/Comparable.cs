using System;

namespace Anonymous
{
    public class Comparable : Comparable<object>, IComparable
    {
        public Comparable(Func<object, int> compareTo)
            : base(compareTo)
        {
        }
    }
}