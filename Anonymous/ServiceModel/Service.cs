using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Anonymous.Infrastructure;

namespace Anonymous.ServiceModel
{
    public abstract class Service : IEquatable<Service>, IEquatable<Delegate>
    {
        private readonly Lazy<Service> component;
        private readonly Delegate delegateInstance;
        private string stringCache;

        protected internal Service(Func<Service> component)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(component, null));
            this.component = new Lazy<Service>(component);
        }

        protected internal Service(Delegate delegateInstance)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(delegateInstance, null));
            this.delegateInstance = delegateInstance;
        }

        private Service Component
        {
            get { return this.component == null ? this : this.component.Value; }
        }

        protected bool IsAtomic
        {
            get { return !IsComposite; }
        }

        protected virtual bool IsComposite
        {
            get { return false; }
        }

        public Delegate Delegate
        {
            get { return Component.delegateInstance; }
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Service) || Equals(obj as Delegate);
        }

        public bool Equals(Service other)
        {
            return AreServicesEqual(this, other);
        }

        private static readonly List<Tuple<int, object, int>> EmptyData = new List<Tuple<int, object, int>>();

        private static List<Tuple<int, object, int>> CanonicalData(Service service)
        {
            if (ReferenceEquals(service, null)) return EmptyData;
            return service
                .GetDelegates()
                .GroupBy(d => Tuple.Create(d.Method.MetadataToken, d.Target))
                .Select(g => Tuple.Create(g.Key.Item1, g.Key.Item2, g.Count()))
                .OrderBy(t => t.Item1)
                .ThenBy(t => (t.Item2 ?? string.Empty).ToString())
                .ThenBy(t => t.Item3)
                .ToList();
        }

        public static bool AreServicesEqual(Service a, Service b)
        {
            if (ReferenceEquals(a, b))
            { 
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            { 
                return false;
            } 
            if (a.IsComposite)
            { 
                if (!b.IsComposite)
                { 
                    var bDelData = b.Delegate.Data();
                    bool b_in_a = a.GetDelegates().Any(bDelData.Equals); 
                    return b_in_a;
                } 
                var aData = CanonicalData(a); 
                var bData = CanonicalData(b); 
                if (aData.Count == bData.Count)
                {
                    return aData.SequenceEqual(bData);
                }
                return false; 
            }
            if (b.IsComposite)
            {
                var aDelData = a.Delegate.Data();
                bool a_in_b = b.GetDelegates().Any(aDelData.Equals); 
                return a_in_b;
            } 
            return a.Delegate.Data().Equals(b.Delegate);
        }

        public sealed override int GetHashCode()
        {
            return InterceptHashCode(GetDelegates().Select(DelegateData.Data).Sum(x => x.GetHashCode()));
        }

        protected internal virtual IEnumerable<Delegate> GetDelegates()
        {
            yield return Delegate;
        }

        protected internal virtual int InterceptHashCode(int defaultHashCode)
        {
            return defaultHashCode;
        }

        public sealed override string ToString()
        {
            return stringCache ?? (stringCache = GetStringRepresentation());
        }
        
        protected internal virtual string GetStringRepresentation()
        {
            return IsAtomic ? Delegate.Data().ToString() : string.Format("({0})", string.Join(",", GetDelegates().Select(DelegateData.Data).Select(x => x.ToString()).OrderBy(x => x)));
        }

        public static bool operator ==(Service service, object obj)
        {
            return ReferenceEquals(service, null) ? ReferenceEquals(obj, null) : service.Equals(obj);
        }

        public static bool operator !=(Service service, object obj)
        {
            return !(service == obj);
        }

        public static bool operator ==(object obj, Service service)
        {
            return service == obj;
        }

        public static bool operator !=(object obj, Service service)
        {
            return service != obj;
        }

        public static bool operator ==(Service a, Service b)
        {
            return AreServicesEqual(a, b);
        }

        public static bool operator !=(Service a, Service b)
        {
            return !(a == b);
        }

        #region a Service equals a Delegate when the Service contains the Delegate's data

        public bool Equals(Delegate other)
        {
            if (other == null) return false;
            var otherData = other.Data();
            return this.IsAtomic ? Delegate.Data().Equals(other) : GetDelegates().Any(otherData.Equals);
        }

        public static bool operator ==(Service service, Delegate d)
        {
            return ReferenceEquals(service, null) ? ReferenceEquals(d, null) : service.Equals(d);
        }

        public static bool operator !=(Service service, Delegate d)
        {
            return !(service == d);
        }

        public static bool operator ==(Delegate d, Service service)
        {
            return service == d;
        }

        public static bool operator !=(Delegate d, Service service)
        {
            return !(d == service);
        }

        #endregion a Service equals a Delegate when the Service contains the Delegate's data
    }
}