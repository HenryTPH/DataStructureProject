using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_A
{
    public class Vertex<T>: IComparable<Vertex<T>>
        where T : IComparable<T>
    {
        #region Attributes
        private T data;
        private int index;
        #endregion

        #region Constructor
        public Vertex(T data, int index)
        {
            this.data = data;
            this.index = index;
        }
        #endregion

        #region Properties
        public T Data { get { return data; } }
        public int Index { get { return index; } set { index = value; } }
        #endregion

        public int CompareTo(Vertex<T> other)
        {
            return Data.CompareTo(other.Data);
        }

        public override string ToString()
        {
            return "[" + data + "(" + index + ")]";
        }

        public override bool Equals(object obj)
        {
            return this.Data.CompareTo(((Vertex<T>)obj).Data) == 0;
        }
    }
}
