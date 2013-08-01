using System;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Observer<T> : TripartiteService<Action<T>, Action<Exception>, Action>, IObserver<T>
    {
        public Observer(Action<T> next, Action<Exception> error, Action completed)
            : base(next, error, completed)
        {
        }

        public void OnNext(T value)
        {
            First.Delegate(value);
        }

        public void OnError(Exception error)
        {
            Second.Delegate(error);
        }

        public void OnCompleted()
        {
            Third.Delegate();
        }
    }
}