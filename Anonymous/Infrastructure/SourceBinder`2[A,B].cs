namespace Anonymous.Infrastructure
{
    using System;
    using System.Diagnostics.Contracts;

    internal class SourceBinder<A, B> : IEquatable<SourceBinder<A, B>>
    {
        private readonly Func<A, B> map;

        private readonly Func<A> source;

        private Func<B> target;

        public SourceBinder(Func<A> source, Func<A, B> map)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(source, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(map, null));
            this.source = source;
            this.map = map;
        }

        public Func<A> Source
        {
            get
            {
                return this.source;
            }
        }

        public Func<A, B> Map
        {
            get
            {
                return this.map;
            }
        }

        public Func<B> Target
        {
            get
            {
                return this.target ?? (this.target = this.Bind);
            }
        }

        public bool Equals(SourceBinder<A, B> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Source.Data().Equals(other.Source) && this.Map.Data().Equals(other.Map);
        }

        public static bool operator ==(SourceBinder<A, B> left, SourceBinder<A, B> right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);
        }

        public static bool operator !=(SourceBinder<A, B> left, SourceBinder<A, B> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Source.GetHashCode() * 397) ^ this.Map.GetHashCode();
            }
        }

        public B Bind()
        {
            return this.Map(this.Source());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(other: obj as SourceBinder<A, B>);
        }

        public override string ToString()
        {
            return new { this.Source, this.Map }.ToString();
        }
    }
}