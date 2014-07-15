namespace Anonymous
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public static class Inline
    {
        public static ICloneable Cloneable(Func<object> clone)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(clone, null));
            return new Cloneable(clone);
        }

        public static IComparable Comparable(Func<object, int> compareTo)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(compareTo, null));
            return new Comparable(compareTo);
        }

        public static IComparable<T> Comparable<T>(Func<T, int> compareTo)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(compareTo, null));
            return new Comparable<T>(compareTo);
        }

        public static IComparable<T> Comparable<T>(T value, IComparer<T> comparer = default(IComparer<T>))
        {
            comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
            return new Comparable<T>(comparand => comparer.Compare(value, comparand));
        }

        public static IComparer Comparer(Comparison<object> compare)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(compare, null));
            return new Comparer(compare);
        }

        public static IComparer<T> Comparer<T>(Comparison<T> compare)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(compare, null));
            return new Comparer<T>(compare);
        }

        public static ICustomFormatter CustomFormatter(Func<object, string> format)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(format, null));
            return new CustomFormatter(format);
        }

        public static ICustomFormatter CustomFormatter(Func<string, object, string> format)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(format, null));
            return new CustomFormatter(format);
        }

        public static ICustomFormatter CustomFormatter(Func<string, object, IFormatProvider, string> format)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(format, null));
            return new CustomFormatter(format);
        }

        public static IDisposable Disposable(Action dispose)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(dispose, null));
            return new Disposable(dispose);
        }

        public static IEnumerable Enumerable(Func<IEnumerator> getEnumerator)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getEnumerator, null));
            return new Enumerable(getEnumerator);
        }

        public static IEnumerable<T> Enumerable<T>(Func<IEnumerator<T>> getEnumerator)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getEnumerator, null));
            return new Enumerable<T>(getEnumerator);
        }

        public static IEnumerator Enumerator(Func<bool> moveNext, Func<object> current, Action reset)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(moveNext, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(current, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(reset, null));
            return new Enumerator(moveNext, current, reset);
        }

        public static IEnumerator<T> Enumerator<T>(Func<bool> moveNext, Func<T> current, Action reset)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(moveNext, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(current, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(reset, null));
            return new Enumerator<T>(moveNext, current, reset);
        }

        public static IEqualityComparer EqualityComparer(
            Func<object, object, bool> equals, Func<object, int> getHashCode)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(equals, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getHashCode, null));
            return new EqualityComparer(equals, getHashCode);
        }

        public static IEqualityComparer<T> EqualityComparer<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(equals, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getHashCode, null));
            return new EqualityComparer<T>(equals, getHashCode);
        }

        public static IEquatable<T> Equatable<T>(Predicate<T> equals)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(equals, null));
            return new Equatable<T>(equals);
        }

        public static IEquatable<T> Equatable<T>(
            T value, IEqualityComparer<T> equalityComparer = default(IEqualityComparer<T>))
        {
            equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
            return new Equatable<T>(candidate => equalityComparer.Equals(value, candidate));
        }

        public static IFormatProvider FormatProvider(Func<Type, object> getFormat)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getFormat, null));
            return new FormatProvider(getFormat);
        }

        public static IFormattable Formattable(Func<string, IFormatProvider, string> toString)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(toString, null));
            return new Formattable(toString);
        }

        public static IFormattable Formattable(Func<string, string> toString)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(toString, null));
            return new Formattable(toString);
        }

        public static IFormattable Formattable(Func<IFormatProvider, string> toString)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(toString, null));
            return new Formattable(toString);
        }

        public static IFormattable Formattable(Func<string> toString)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(toString, null));
            return new Formattable(toString);
        }

        public static IGrouping<TKey, TElement> Grouping<TKey, TElement>(
            Func<IEnumerator<TElement>> getEnumerator, Func<TKey> key)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getEnumerator, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(key, null));
            return new Grouping<TKey, TElement>(getEnumerator, key);
        }

        public static IObservable<T> Observable<T>(Func<IObserver<T>, IDisposable> subscribe)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(subscribe, null));
            return new Observable<T>(subscribe);
        }

        public static IObserver<T> Observer<T>(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(onNext, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(onError, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(onCompleted, null));
            return new Observer<T>(onNext, onError, onCompleted);
        }

        public static IServiceProvider ServiceProvider(Func<Type, object> getService)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getService, null));
            return new ServiceProvider(getService);
        }

        public static IStructuralComparable StructuralComparable(Func<object, IComparer, int> compareTo)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(compareTo, null));
            return new StructuralComparable(compareTo);
        }

        public static IStructuralEquatable StructuralEquatable(
            Func<object, IEqualityComparer, bool> equals, Func<IEqualityComparer, int> getHashCode)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(equals, null));
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(getHashCode, null));
            return new StructuralEquatable(equals, getHashCode);
        }
    }
}