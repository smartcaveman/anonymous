using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class EqualityComparer<T> : BipartiteService<Func<T, T, bool>, Func<T, int>>, IEqualityComparer<T>
    {
        private readonly Func<T, int> getHashCode;
        private readonly Func<T, T, bool> equals;

        public EqualityComparer(Func<T, T, bool> @equals, Func<T, int> getHashCode)
            : base(equals, getHashCode)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(equals, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getHashCode, null));
            this.getHashCode = getHashCode;
            this.equals = equals;
        }

        public bool Equals(T x, T y)
        {
            return this.equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return this.getHashCode(obj);
        }

        public static implicit operator Func<T, T, bool>(EqualityComparer<T> comparer)
        {
            return ReferenceEquals(comparer, null) ? null : comparer.equals;
        }

        public static implicit operator Func<T, int>(EqualityComparer<T> comparer)
        {
            return ReferenceEquals(comparer, null) ? null : comparer.getHashCode;
        }
    }
}