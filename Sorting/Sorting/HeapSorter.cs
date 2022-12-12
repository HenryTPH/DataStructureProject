using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class HeapSorter<T>: ASorter<T>
        where T: IComparable<T>
    {
        public HeapSorter(T[] array) : base(array) { }
        //In heapsort we have 2 work to do
        //1. Make all element or array become a max heap or min heap
        //2. Remove each element to sorting them
        public override void Sort()
        {
            Heapify();
            for(int i = array.Length - 1; i > 0; i--)
            {
                RemoveNextMax(i);
            }
        }
        private void RemoveNextMax(int lastPos)
        {
            T max = array[0];
            array[0] = array[lastPos];
            array[lastPos] = max;
            TrickleDown(0, lastPos - 1);
        }
        private void Heapify()
        {
            int parentIndex = GetParentIndex(array.Length - 1);
            for(int index = parentIndex; index >= 0; index--)
            {
                TrickleDown(index, array.Length - 1);
            }
        }
        private void TrickleDown(int index, int lastPos)
        {
            T current = array[index];
            int largerChildIndex = GetLeftChildIndex(index);
            bool done = false;
            while(!done && largerChildIndex <= lastPos)
            {
                int rightChildIndex = GetRightchildIndex(index);
                if(rightChildIndex <= lastPos && array[rightChildIndex].CompareTo(array[largerChildIndex]) > 0)
                {
                    largerChildIndex = rightChildIndex;
                }
                if (current.CompareTo(array[largerChildIndex] ) < 0)
                {
                    array[index] = array[largerChildIndex];
                    index = largerChildIndex;
                    largerChildIndex = GetLeftChildIndex(index);
                }
                else
                {
                    done = true;
                }
            }
            array[index] = current;
        }
        private int GetParentIndex(int index)
        {
            return (index - 1)/2;
        }
        private int GetLeftChildIndex(int index)
        {
            return 2 * index + 1;
        }
        private int GetRightchildIndex(int index)
        {
            return 2 * index + 2;
        }
    }
}
