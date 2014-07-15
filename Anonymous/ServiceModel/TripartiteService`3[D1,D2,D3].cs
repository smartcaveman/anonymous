namespace Anonymous.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public class TripartiteService<D1, D2, D3> : Service<D1>, IEquatable<D2>
        where D1 : class
        where D2 : class
        where D3 : class
    {
        private readonly Lazy<TripartiteService<D2, D3, D1>> left;

        public TripartiteService(D1 first, D2 second, D3 third)
            : this(first, second, (AtomicService<D3>)third)
        {
        }

        public TripartiteService(AtomicService<D1> first, AtomicService<D2> second, AtomicService<D3> third)
            : base(first)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(second, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(third, null));
            this.left =
                new Lazy<TripartiteService<D2, D3, D1>>(
                    () =>
                    new TripartiteService<D2, D3, D1>(
                        second, ccw => new TripartiteService<D3, D1, D2>(third, x => this)));
        }

        private TripartiteService(
            AtomicService<D1> primaryService,
            Func<TripartiteService<D1, D2, D3>, TripartiteService<D2, D3, D1>> counterClockwise)
            : base(primaryService)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(counterClockwise, null));
            this.left = new Lazy<TripartiteService<D2, D3, D1>>(() => counterClockwise(this));
        }

        public Service<D1> First
        {
            get
            {
                return this;
            }
        }

        public Service<D2> Second
        {
            get
            {
                return this.Next();
            }
        }

        public Service<D3> Third
        {
            get
            {
                return this.Next().Next();
            }
        }

        protected override sealed bool IsComposite
        {
            get
            {
                return true;
            }
        }

        public bool Equals(D2 other)
        {
            return this.Next().Equals(other);
        }

        public TripartiteService<D3, D1, D2> Previous()
        {
            return this.Next().Next();
        }

        public TripartiteService<D2, D3, D1> Next()
        {
            return this.left.Value;
        }

        public bool Equals(D3 other)
        {
            return this.Previous().Equals(other);
        }

        protected internal override IEnumerable<Delegate> GetDelegates()
        {
            yield return ((Service)this.First).Delegate;
            yield return ((Service)this.Second).Delegate;
            yield return ((Service)this.Third).Delegate;
        }

        protected internal override int InterceptHashCode(int defaultHashCode)
        {
            return defaultHashCode ^ this.Second.GetHashCode() ^ this.Third.GetHashCode();
        }
    }
}