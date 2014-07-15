namespace Anonymous
{
    using Anonymous.ServiceModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Enumerator<T> : TripartiteService<Func<bool>, Func<T>, Action>, IEnumerator<T>
    {
        public Enumerator(Func<bool> moveNext, Func<T> current, Action reset)
            : base(moveNext, current, reset)
        {
        }

        public T Current
        {
            get
            {
                return this.Second.Delegate();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public bool MoveNext()
        {
            return this.Delegate();
        }

        public void Reset()
        {
            this.Third.Delegate();
        }

        public void Dispose()
        {
            this.Third.Delegate();
        }
    }
}