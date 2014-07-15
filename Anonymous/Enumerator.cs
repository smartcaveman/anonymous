namespace Anonymous
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Enumerator : Enumerator<object>
    {
        public Enumerator(Func<bool> moveNext, Func<object> current, Action reset)
            : base(moveNext, current, reset)
        {
        }

        public static IEnumerator<object> Generic(IEnumerator enumerator)
        {
            if (enumerator == null)
            {
                yield break;
            }
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}