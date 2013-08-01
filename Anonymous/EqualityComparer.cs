using System;
using System.Collections;

namespace Anonymous
{
    public sealed class EqualityComparer : EqualityComparer<object>,IEqualityComparer{
        public EqualityComparer(Func<object, object, bool> @equals, Func<object, int> getHashCode) : base(@equals, getHashCode)
        {
        }
    }
}