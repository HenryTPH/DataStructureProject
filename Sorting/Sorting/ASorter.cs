using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public abstract class ASorter<T>
        where T: IComparable<T>
    {
        protected T[] array;
        public ASorter(T[] array) => this.array = array;
        public abstract void Sort();
        protected void Swap(int first, int second)
        {
            T temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
        //How to implement [] on a class
        public T this[int index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }
        public int Length
        {
            get { return array.Length; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            foreach (T item in array)
            {
                sb.Append(item);
                sb.Append(', ');
            }
            if(sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}
