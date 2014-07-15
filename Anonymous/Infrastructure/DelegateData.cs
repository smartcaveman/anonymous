namespace Anonymous.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal static class DelegateData
    {
        public static readonly IEqualityComparer<Delegate> Equivalence = new EqualityComparer();

        public static IEquatable<Delegate> Data(this Delegate a)
        {
            return Value.For(a);
        }

        private class EqualityComparer : EqualityComparer<Delegate>
        {
            public static readonly IEqualityComparer<Delegate> Comparer = new EqualityComparer();

            public override bool Equals(Delegate a, Delegate b)
            {
                return ReferenceEquals(a, b) || Value.For(a).Equals(b);
            }

            public override int GetHashCode(Delegate obj)
            {
                return obj == null ? 0 : new { obj.Method, obj.Target }.GetHashCode();
            }
        }

        private abstract class Value : IEquatable<Value>, IEquatable<Delegate>
        {
            private static readonly Value Empty = For(null);

            private readonly object _data;

            private Value(MethodInfo Method, object Target)
            {
                this._data = new { Method, Target };
            }

            protected abstract Type ReturnType { get; }

            public bool Equals(Delegate other)
            {
                return this.Equals(For(other));
            }

            public bool Equals(Value other)
            {
                other = other ?? Empty;
                return this._data.Equals(other._data) && CompatibleTypes(this.ReturnType, other.ReturnType);
            }

            public static Value For(Delegate d)
            {
                if (d == null)
                {
                    return Empty ?? new WithReturn<object>(null, null);
                }
                return (Value)Activator.CreateInstance(Type(d.Method), new[] { d.Method, d.Target });
            }

            public override sealed bool Equals(object obj)
            {
                return this.Equals(obj as Delegate) || this.Equals(obj as Value);
            }

            public override sealed int GetHashCode()
            {
                return this.Equals(Empty) ? 0 : this._data.GetHashCode();
            }

            public override string ToString()
            {
                return this._data.ToString();
            }

            private static Type Type(MethodInfo method)
            {
                return
                    typeof(WithReturn<>).MakeGenericType(
                        method.ReturnType == typeof(void) ? typeof(_void_) : method.ReturnType);
            }

            private static bool CompatibleTypes(Type a, Type b)
            {
                if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                {
                    return false;
                }
                return a.IsAssignableFrom(b) || b.IsAssignableFrom(a);
            }

            private class WithReturn<T> : Value
            {
                public WithReturn(MethodInfo method, object target)
                    : base(Method: method, Target: target)
                {
                }

                protected override sealed Type ReturnType
                {
                    get
                    {
                        return typeof(T);
                    }
                }
            }

            private struct _void_
            {
            }
        }
    }
}