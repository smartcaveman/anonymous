using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class ServiceProvider : AtomicService<Func<Type, object>>, IServiceProvider
    {
        public ServiceProvider(Func<Type, object> getService)
            : base(getService)
        {
        }

        public object GetService(Type serviceType)
        {
            return Delegate(serviceType);
        }
    }
}