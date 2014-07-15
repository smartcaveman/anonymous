namespace Anonymous.ServiceModel
{
    using System;

    public class AtomicService<D> : Service<D>, IEquatable<D>
        where D : class
    {
        internal AtomicService(D delegateInstance)
            : base(delegateInstance)
        {
        }

        protected AtomicService(AtomicService<D> component)
            : this(() => component)
        {
        }

        protected AtomicService(Func<AtomicService<D>> component)
            : base(component)
        {
        }

        public static implicit operator AtomicService<D>(D delegateInstance)
        {
            Service<D> service = delegateInstance;
            return (AtomicService<D>)service;
        }
    }
}