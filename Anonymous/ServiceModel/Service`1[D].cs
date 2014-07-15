namespace Anonymous.ServiceModel
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics.Contracts;

    public abstract class Service<D> : Service
        where D : class
    {
        private static readonly ConcurrentDictionary<D, AtomicService<D>> DelegateServiceMap;

        private static readonly Func<D, AtomicService<D>> NewDelegateService;

        static Service()
        {
            Contract.Assert(typeof(Delegate).IsAssignableFrom(typeof(D)));
            DelegateServiceMap = new ConcurrentDictionary<D, AtomicService<D>>();
            NewDelegateService = d => new AtomicService<D>(d);
        }

        protected internal Service(Service<D> component)
            : base(() => component)
        {
        }

        protected internal Service(Func<Service<D>> component)
            : base(component)
        {
        }

        protected internal Service(D delegateInstance)
            : base(delegateInstance as Delegate)
        {
        }

        public new D Delegate
        {
            get
            {
                Contract.Ensures(Contract.Result<D>() is Delegate);
                return base.Delegate as D;
            }
        }

        public static implicit operator Service<D>(D delegateInstance)
        {
            return ReferenceEquals(delegateInstance, null)
                       ? null
                       : DelegateServiceMap.GetOrAdd(delegateInstance, NewDelegateService);
        }

        public bool Equals(D other)
        {
            return this.Equals(other as Delegate);
        }
    }
}