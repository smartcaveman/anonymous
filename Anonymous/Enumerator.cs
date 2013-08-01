using System;
using System.Collections;
using System.Collections.Generic;

namespace Anonymous
{
    public class Enumerator : Enumerator<object>
    {
        public static IEnumerator<object> Generic(IEnumerator enumerator)
        {
            if(enumerator == null)
                yield break;
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
        public Enumerator(Func<bool> moveNext, Func<object> current, Action reset) : base(moveNext, current, reset)
        {
        }
    }
}