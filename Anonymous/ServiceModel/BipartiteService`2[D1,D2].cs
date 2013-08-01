using System;
using System.Diagnostics.Contracts;

namespace Anonymous.ServiceModel
{
    public class BipartiteService<D1, D2> : Service<D1>, IEquatable<D2>
        where D1 : class
        where D2 : class
    {
        private readonly BipartiteService<D2, D1> compositionDual;

        public BipartiteService(D1 primaryDelegateInstance, D2 secondaryDelegateInstance)
            : this((AtomicService<D1>)primaryDelegateInstance, (AtomicService<D2>)secondaryDelegateInstance) { }

        public BipartiteService(AtomicService<D1> first, AtomicService<D2> second)
            : base(first)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(second, null));
            this.compositionDual = new BipartiteService<D2, D1>(second, this);
        }

        private BipartiteService(AtomicService<D1> first, BipartiteService<D2, D1> compositionDual)
            : base(first)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(compositionDual, null));
            this.compositionDual = compositionDual;
        }

        public Service<D1> First { get { return this; } }

        public Service<D2> Second { get { return Next().First; } }

        public BipartiteService<D2, D1> Previous()
        {
            return this.compositionDual;
        }

        public BipartiteService<D2, D1> Next()
        {
            return this.compositionDual;
        }

        protected sealed override bool IsComposite { get { return true; } }

        protected internal override System.Collections.Generic.IEnumerable<Delegate> GetDelegates()
        {
            yield return ((Service)(First)).Delegate;
            yield return ((Service)(Second)).Delegate;
        }

        public bool Equals(D2 other)
        {
            return Next().Equals(other);
        }

        protected internal override int InterceptHashCode(int defaultHashCode)
        {
            return defaultHashCode ^ Second.GetHashCode();
        }
    }
}