namespace Anonymous
{
    using System;

    using Anonymous.ServiceModel;

    public class Disposable : AtomicService<Action>, IDisposable
    {
        public Disposable(Action dispose)
            : base(dispose)
        {
        }

        public void Dispose()
        {
            this.Delegate.Invoke();
        }
    }
}