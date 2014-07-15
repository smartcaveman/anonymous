namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;

    public class Observer<T> : TripartiteService<Action<T>, Action<Exception>, Action>, IObserver<T>
    {
        public Observer(Action<T> next, Action<Exception> error, Action completed)
            : base(next, error, completed)
        {
        }

        public void OnNext(T value)
        {
            this.First.Delegate(value);
        }

        public void OnError(Exception error)
        {
            this.Second.Delegate(error);
        }

        public void OnCompleted()
        {
            this.Third.Delegate();
        }
    }
}