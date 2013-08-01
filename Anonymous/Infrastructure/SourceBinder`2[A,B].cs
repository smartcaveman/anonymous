using System;
using System.Diagnostics.Contracts;

namespace Anonymous.Infrastructure
{
    internal class SourceBinder<A, B> : IEquatable<SourceBinder<A, B>>
    {
        public bool Equals(SourceBinder<A, B> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Source.Data().Equals(other.Source) && this.Map.Data().Equals(other.Map);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Source.GetHashCode() * 397) ^ Map.GetHashCode();
            }
        }

        public static bool operator ==(SourceBinder<A, B> left, SourceBinder<A, B> right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);
        }

        public static bool operator !=(SourceBinder<A, B> left, SourceBinder<A, B> right)
        {
            return !(left == right);
        }

        private readonly Func<A> source;
        private readonly Func<A, B> map;
        private Func<B> target;

        public SourceBinder(Func<A> source, Func<A, B> map)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(source, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(map, null));
            this.source = source;
            this.map = map;
        }

        public B Bind()
        {
            return Map(Source());
        }

        public Func<A> Source
        {
            get { return source; }
        }

        public Func<A, B> Map
        {
            get { return this.map; }
        }

        public Func<B> Target
        {
            get { return target ?? (target = Bind); }
        }

        public override bool Equals(object obj)
        {
            return Equals(other: obj as SourceBinder<A, B>);
        }

        public override string ToString()
        {
            return new { Source, Map }.ToString();
        }
    }
}