namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class ServiceProvider : AtomicService<Func<Type, object>>, IServiceProvider
    {
        public ServiceProvider(Func<Type, object> getService)
            : base(getService)
        {
        }

        public object GetService(Type serviceType)
        {
            return this.Delegate(serviceType);
        }
    }
}