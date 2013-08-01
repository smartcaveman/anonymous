using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Grouping<TKey,TElement> : BipartiteService<Func<IEnumerator<TElement>>,Func<TKey>>, IGrouping<TKey,TElement>
    {
        public Grouping(Func<IEnumerator<TElement>> getEnumerator, Func<TKey> key)
            : base(getEnumerator, key)
        { 
        } 
        public IEnumerator<TElement> GetEnumerator()
        {
            return this.First.Delegate.Invoke();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.First.Delegate.Invoke();
        }

        public TKey Key
        {
            get { return Second.Delegate.Invoke(); }
        }

    }
}