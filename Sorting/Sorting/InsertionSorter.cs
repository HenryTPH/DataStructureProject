using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class InsertionSorter<T>: ASorter<T> where T : IComparable<T>
    {
        public InsertionSorter(T[] array): base(array) { }
        public override void Sort()
        {
            for(int i = 1; i < array.Length; i++)
            {
                InsertInOrder(i);
            }
        }
        private void InsertInOrder(int indexUnsorted)
        {
            T unSortedElement = array[indexUnsorted];
            int index = indexUnsorted - 1;
            while(index >= 0 && unSortedElement.CompareTo(array[index]) < 0)
            {
                array[index + 1] = array[index];
                index--;
            }
            array[index + 1] = unSortedElement;
        }
    }
}
