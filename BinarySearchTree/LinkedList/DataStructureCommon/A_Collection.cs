using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureCommon
{
    public abstract class A_Collection<T> :I_Collection<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Add(T data);
        public abstract void Clear();
        public abstract bool Remove(T data);
        public abstract IEnumerator<T> GetEnumerator();
        #endregion

        //Recall that Count is a property, a C# construct similar to a getter and setter
        //Note that the current implementation of Count is propaby not very efficient
        //Therefore, we want the ability to override this property in a child implementation
        //The keypord "virtual" allows this to occur
        public virtual int Count 
        {
            get 
            { 
                int count = 0;
                
                foreach (T item in this)
                    count++;
                    return count;
            }
        }

        public bool Contains(T data)
        {
            bool found = false;
            //IEnumerator<T> myEnum = GetEnumerator();
            //myEnum.Reset();
            //while(!found && myEnum.MoveNext())
            //{
                //found = myEnum.Current.Equals(data);
                //if(myEnum.Current.CompareTo(data) == 0)
                    //found = true;
            //}
            foreach(T item in this)
            {
                if (item.CompareTo(data) == 0)
                {
                    found = true;
                }
            }
            return found;
        }

        //Override the implementation of ToString. Typically, this method would not 
        //be part of a data structure, at least not this implementation where
        //all data items are appended to the string. Could be really long...
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
                result.Remove(result.Length - sep.Length, sep.Length);
            }

            result.Append("}");

            return result.ToString();
        }


        //A default implementation so we don't have to worry about this method
        //For non-generic implementation
        //IEnumerable requires that you implement both generic and non-generic
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
