using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Anonymous.ServiceModel
{
    public class TripartiteService<D1, D2, D3> : Service<D1>, IEquatable<D2>
        where D1 : class
        where D2 : class
        where D3 : class 
    { 
        private readonly Lazy<TripartiteService<D2, D3, D1>> left;
        public TripartiteService(D1 first, D2 second, D3 third) 
            : this((AtomicService<D1>)first, (AtomicService<D2>)second, (AtomicService<D3>)third) { }

        public TripartiteService(AtomicService<D1> first, AtomicService<D2> second, AtomicService<D3> third)
            : base(first)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(second, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(third, null));
            this.left = 
                new Lazy<TripartiteService<D2, D3, D1>>(
                    () => new TripartiteService<D2, D3, D1>(second, 
                                                           ccw => new TripartiteService<D3, D1, D2>(third,x => this))); 
        }


        private TripartiteService(AtomicService<D1> primaryService, Func<TripartiteService<D1, D2, D3>, TripartiteService<D2, D3, D1>> counterClockwise)
            : base(primaryService)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(counterClockwise, null));
            this.left = new Lazy<TripartiteService<D2, D3, D1>>(() => counterClockwise(this)); 
        }


        public Service<D1> First { get { return this; } }
        public Service<D2> Second { get { return Next(); } }
        public Service<D3> Third { get { return Next().Next(); } }
        public TripartiteService<D3, D1, D2> Previous() { return Next().Next(); }
        public TripartiteService<D2, D3, D1> Next() { return this.left.Value; } 
        protected sealed override bool IsComposite { get { return true; } }
        protected internal override System.Collections.Generic.IEnumerable<Delegate> GetDelegates()
        {
            yield return ((Service)(First)).Delegate;
            yield return ((Service)(Second)).Delegate;
            yield return ((Service)(Third)).Delegate; 
        }
        public bool Equals(D2 other) { return Next().Equals(other); }
        public bool Equals(D3 other) { return Previous().Equals(other); }
        protected internal override int InterceptHashCode(int defaultHashCode) { return defaultHashCode ^ Second.GetHashCode() ^ Third.GetHashCode(); }
    }
}