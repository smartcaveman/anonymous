using System;
using System.Collections;
using System.Collections.Generic;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class Enumerator<T> : TripartiteService<Func<bool>, Func<T>, Action>, IEnumerator<T>
    {
        public Enumerator(Func<bool> moveNext, Func<T> current, Action reset)
            : base(moveNext, current, reset)
        {
        }

        public bool MoveNext()
        {
            return Delegate();
        }

        public T Current
        {
            get { return Second.Delegate(); }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Reset()
        {
            Third.Delegate();
        }

        public void Dispose()
        {
            Third.Delegate();
        }
    }
}