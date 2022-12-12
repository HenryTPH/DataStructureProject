using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSorterMediumOfTree<T>: QuickSorter<T>
        where T: IComparable<T>
    {
        public QuickSorterMediumOfTree(T[] array): base(array) { }
        protected override int FindPivot(int first, int last)
        {
            int mid = (first + last) / 2;
            if (array[mid].CompareTo(array[first]) > 0 && array[mid].CompareTo(array[last]) < 0)
            {
                return mid;
            }
            else if (array[mid].CompareTo(array[first]) < 0 && array[first].CompareTo(array[last]) < 0)
            {
                return first;
            }
            else
            {
                return last;
            }
        }
    }
}
