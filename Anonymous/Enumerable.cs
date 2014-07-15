namespace Anonymous
{
    using Anonymous.Infrastructure;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Enumerable : Enumerable<object>
    {
        public Enumerable(Func<IEnumerator> getEnumerator)
            : this(new SourceBinder<IEnumerator, IEnumerator<object>>(getEnumerator, Enumerator.Generic).Target)
        {
            ;
        }

        public Enumerable(Func<IEnumerator<object>> getEnumerator)
            : base(getEnumerator)
        {
        }
    }
}