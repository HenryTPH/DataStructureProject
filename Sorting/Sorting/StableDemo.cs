using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class StableDemo: IComparable<StableDemo>
    {
        private int value;
        private int initialIndex;
        public StableDemo(int value, int initialIndex)
        {
            this.value=value;
            this.initialIndex=initialIndex;
        }
        public int CompareTo(StableDemo other)
        {
            return value.CompareTo(other.value);
        }
        public override string ToString()
        {
            return "(" + value + ", " + initialIndex + ")";
        }
    }
}
