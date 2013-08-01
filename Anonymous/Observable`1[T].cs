using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Observable<T> : AtomicService<Func<IObserver<T>, IDisposable>>, IObservable<T>
    {
        public Observable(Func<IObserver<T>, IDisposable> subscribe)
            : base(subscribe) { }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return Delegate.Invoke(observer);
        }
    }
}