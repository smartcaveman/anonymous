using System;
using System.Collections;
using System.Collections.Generic;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Enumerable<T> : AtomicService<Func<IEnumerator<T>>>, IEnumerable<T>
    {
        public Enumerable(Func<IEnumerator<T>> getEnumerator)
            : base(getEnumerator)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Delegate.Invoke();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Delegate.Invoke();
        }
    }
}