using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructureCommon;

namespace LinkedList
{
    public abstract class A_List<T>: A_Collection<T>, I_List<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Insert(int index, T data);
        public abstract T RemoveAt(int index);
        public abstract T ReplaceAt(int index, T data);
        #endregion

        #region I_LIST implementation
        public T ElementAt(int index)
        {
            T tOriginal = default(T);

            int  count = 0;

            if(index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Invalid Index");
            }

            IEnumerator<T> myEnum = this.GetEnumerator();

            myEnum.Reset();

            while (myEnum.MoveNext() && count != index)
            {
                count++;
            }

            tOriginal = myEnum.Current;
            return tOriginal;
        }

        public int IndexOf(T data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
