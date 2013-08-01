using System;
using System.Collections;

namespace Anonymous
{
    public class Comparer : Comparer<object>, IComparer
    {
        public Comparer(Comparison<object> compare)
            : base(compare)
        {
        }
    }
}