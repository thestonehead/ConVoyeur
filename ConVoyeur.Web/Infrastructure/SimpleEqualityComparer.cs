using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Infrastructure
{
    public class SimpleEqualityComparer<T, TId> : IEqualityComparer<T> where TId : struct
    {
        private readonly Func<T, TId> idSelector;

        public SimpleEqualityComparer(Func<T, TId> idSelector)
        {
            this.idSelector = idSelector;
        }

        public bool Equals(T x, T y)
        {
            return this.idSelector(x).Equals(this.idSelector(y));
        }

        public int GetHashCode(T obj)
        {
            return this.idSelector(obj).GetHashCode();
        }
    }
}
