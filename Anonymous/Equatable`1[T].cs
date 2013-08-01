using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Equatable<T> : AtomicService<Predicate<T>>, IEquatable<T>
    {
        public Equatable(Predicate<T> equals)
            : base(equals)
        {
        }

        public bool Equals(T other)
        {
            return Delegate(other);
        }
    }
}