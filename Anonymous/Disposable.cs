using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Disposable : AtomicService<Action>, IDisposable
    {
        public Disposable(Action dispose)
            : base(dispose) { }

        public void Dispose()
        {
            Delegate.Invoke();
        }
    }
}