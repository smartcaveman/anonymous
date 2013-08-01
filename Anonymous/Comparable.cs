using System;

namespace Anonymous
{
    public class Comparable : Comparable<object>, IComparable
    {
        protected Comparable(Func<object, int> component) : base(component)
        {
        }
    }
}