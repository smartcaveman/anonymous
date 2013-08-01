using System;
using System.Collections;
using Anonymous.ServiceModel;

namespace Anonymous
{
    public class StructuralEquatable : BipartiteService<Func<object, IEqualityComparer, bool>,Func<IEqualityComparer,int>>, IStructuralEquatable
    {
        public StructuralEquatable(Func<object, IEqualityComparer, bool> equals, Func<IEqualityComparer, int> getHashCode) 
            : base(equals, getHashCode)
        {
        }
         
        public bool Equals(object other, IEqualityComparer comparer)
        {
            return First.Delegate(other, comparer);
        }

        public int GetHashCode(IEqualityComparer comparer)
        {
            return Second.Delegate(comparer);
        }
    }
}