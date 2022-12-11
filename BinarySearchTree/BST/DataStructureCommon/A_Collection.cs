using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureCommon
{
    public abstract class A_Collection<T>: I_Collection<T> where T: IComparable<T>
    {
        #region Abstract methods
        public abstract void Add(T data);
        public abstract void Clear();
        public abstract bool Remove(T data);
        public abstract IEnumerator<T> GetEnumerator();
        #endregion
        public virtual int Count
        {
            get
            {
                int count = 0;
                foreach(T item in this)
                {
                    count++;
                }
                return count;
            }
        }
        public bool Contains(T data)
        {
            bool found = false;
            /*IEnumerator<T> enumerator = this.GetEnumerator();
            enumerator.Reset();
            while(!found && enumerator.MoveNext())
            {
                found = enumerator.Current.Equals(data);
                if(enumerator.Current.CompareTo(data) == 0)
                    found= true;
            }*/
            //We can use the above method, but there is another way
            foreach(T item in this)
            {
                if (item.CompareTo(data) == 0)
                {
                    found= true;
                }
            }
            return found;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("{");
            string sep = ", ";
            foreach(T item in this)
            {
                result.Append(item + sep);
            }
            if(Count > 0)
            {
                result.Remove(result.Length - 2, 2);
            }
            result.Append("}");
            return result.ToString();
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
