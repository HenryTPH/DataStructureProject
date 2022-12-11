using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureCommon
{
    public interface I_Collection<T>: IEnumerable<T> where T: IComparable<T>
    {
        void Add(T data);
        void Clear();
        bool Contains(T data);
        bool Equals(Object other);
        bool Remove(T data);
        int Count { get; }
    }
}
