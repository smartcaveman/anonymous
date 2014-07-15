namespace Anonymous
{
    using System;
    using System.Collections;

    public class Comparer : Comparer<object>, IComparer
    {
        public Comparer(Comparison<object> compare)
            : base(compare)
        {
        }
    }
}