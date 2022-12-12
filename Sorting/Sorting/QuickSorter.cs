using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSorter<T>: ASorter<T>
        where T : IComparable<T>
    {
        public QuickSorter(T[] array) : base(array) { }
        public override void Sort()
        {
            QuickSortRec(0, array.Length - 1);
        }
        protected virtual int FindPivot(int first, int last)
        {
            return last;
        }
        private int Partition(int left, int right, T pivotValue)
        {
            do
            {
                //Move left pointer until we find a value greater than the pivotValue
                while (array[++left].CompareTo(pivotValue) < 0) ;
                //Move right pointer toward left until we find a value smaller than the pivotValue
                while(right > left && array[--right].CompareTo(pivotValue) > 0) ;
                Swap(left, right);
            }while(left < right);
            return left;
        }
        private void QuickSortRec(int first, int last)
        {
            //Base case, 1 element - do nothing
            if(last - first > 0)
            {
                //Find the location of the pivot
                int pivotIndex = FindPivot(first, last);
                //Move the pivot to the rightmost position
                Swap(pivotIndex, last);
                //Partition relation to the pivot value
                int partitionIndex = Partition(first - 1, last, array[last]);
                //Swap the pivot into what the partitionIndex points to
                Swap(partitionIndex, last);
                //If the subarrays are large enoughm swap different thread for each recursive call
                if(last - first > 1000)
                {
                    Parallel.Invoke(
                        () => QuickSortRec(first, partitionIndex - 1),
                        () => QuickSortRec(partitionIndex+1, last));
                }
                else
                {
                    QuickSortRec(first, partitionIndex - 1);
                    QuickSortRec(partitionIndex+1, last);
                }
            }
        }
    }
}
