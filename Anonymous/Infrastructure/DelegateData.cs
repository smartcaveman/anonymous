using System;
using System.Collections.Generic;
using System.Reflection;

namespace Anonymous.Infrastructure
{
    internal static class DelegateData
    {
        public static readonly IEqualityComparer<Delegate> Equivalence = new EqualityComparer();
        public static IEquatable<Delegate> Data(this Delegate a) { return Value.For(a); }
        private abstract class Value : IEquatable<Value>, IEquatable<Delegate>
        {
            private struct _void_{}

            private static readonly Value Empty = For(null);
            public static Value For(Delegate d)
            {
                if (d == null) return Empty ?? new WithReturn<object>(null, null);
                return (Value)Activator.CreateInstance(Type(d.Method),new[] {d.Method, d.Target});
            }
            static Type Type(MethodInfo method) { return typeof(WithReturn<>).MakeGenericType(method.ReturnType == typeof(void) ? typeof(_void_) : method.ReturnType); }
            private readonly object _data;
            private Value(MethodInfo Method, object Target) { this._data = new { Method, Target }; } 
            protected abstract Type ReturnType { get; }
            public bool Equals(Delegate other) { return Equals(Value.For(other)); } 
            public sealed override bool Equals(object obj) { return Equals(obj as Delegate) || Equals(obj as Value); }
            public bool Equals(Value other)
            {
                other = other ?? Empty;
                return _data.Equals(other._data)
                       && CompatibleTypes(ReturnType, other.ReturnType); 
            }
            static bool CompatibleTypes(Type a, Type b)
            {
                if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
                return a.IsAssignableFrom(b) || b.IsAssignableFrom(a);
            }
            public sealed override int GetHashCode() { return Equals(Empty) ? 0 : _data.GetHashCode(); }
            public override string ToString(){ return _data.ToString(); } 
            private class WithReturn<T> : Value
            {
                public WithReturn(MethodInfo method, object target) : base(Method:method,Target:target){}
                protected sealed override Type ReturnType{ get { return typeof (T); } } 
            }
        }
        private class EqualityComparer : System.Collections.Generic.EqualityComparer<Delegate>
        {
            public static readonly IEqualityComparer<Delegate> Comparer = new EqualityComparer(); 
            public override bool Equals(Delegate a, Delegate b){ return ReferenceEquals(a, b) || Value.For(a).Equals(b); }
            public override int GetHashCode(Delegate obj) { return obj == null ? 0 : new {obj.Method, obj.Target}.GetHashCode(); }
        }
    }
}