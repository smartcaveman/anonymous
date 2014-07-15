namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class Observable<T> : AtomicService<Func<IObserver<T>, IDisposable>>, IObservable<T>
    {
        public Observable(Func<IObserver<T>, IDisposable> subscribe)
            : base(subscribe)
        {
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.Delegate.Invoke(observer);
        }
    }
}