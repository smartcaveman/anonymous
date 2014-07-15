namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class Equatable<T> : AtomicService<Predicate<T>>, IEquatable<T>
    {
        public Equatable(Predicate<T> equals)
            : base(equals)
        {
        }

        public bool Equals(T other)
        {
            return this.Delegate(other);
        }
    }
}