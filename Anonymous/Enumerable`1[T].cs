namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Enumerable<T> : AtomicService<Func<IEnumerator<T>>>, IEnumerable<T>
    {
        public Enumerable(Func<IEnumerator<T>> getEnumerator)
            : base(getEnumerator)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Delegate.Invoke();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Delegate.Invoke();
        }
    }
}