Anonymous.NET
=============

A utility library containing delegate-based anonymous service implementations for many of .NET's core interfaces.

Implemented Interfaces
----------------------

- `ICloneable`
- `IComparable` / `IComparable<T>`
- `IComparer` / `IComparer<T>`
- `ICustomFormatter`
- `IDisposable`
- `IEnumerable` / `IEnumerable<T>`
- `IEnumerator` / `IEnumerator<T>`
- `IEqualityComparer` / `IEqualityComparer<T>`
- `IEquatable<T>`
- `IFormatProvider`
- `IFormattable`
- `IGrouping<TKey,TElement>`
- `IObservable<T>`
- `IObserver<T>`
- `IServiceProvider`
- `IStructuralComparable`
- `IStructuralEquatable`


Examples:
 
 
        static void Main()
        {
            // return an anonymous IDisposable from the Subscribe method 
            // of an anonymous IObservable<T> 
            Inline.Observable<int>(observer => Inline.Disposable(observer.OnCompleted)); 
            
            // use a custom equality comparer for identifying keys in a dictionary
            Func<string, string> sanitize = a => (a ?? string.Empty).Replace(' ', '-');
            new Dictionary<string, object>(Inline.EqualityComparer<string>((a, b) => sanitize(a) == sanitize(b), a => sanitize(a).GetHashCode())); 
         } 
