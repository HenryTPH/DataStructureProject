using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public class Node<T> where T: IComparable<T>
    {
        private T data;
        private Node<T> nLeft;
        private Node<T> nRight;
        public Node(T data, Node<T> nLeft, Node<T> nRight)
        {
            this.data=data;
            this.nLeft=nLeft;
            this.nRight=nRight;
        }
        public Node(): this(default(T), null, null) { }
        public Node(T data): this(data, null, null) { }
        public T Data { get { return data; } set { data = value; } }
        public Node<T> Left { get { return nLeft; } set { nLeft = value; } }
        public Node<T> Right { get { return nRight; } set { nRight = value; } }
        public bool IsLeaf()
        {
            return this.Left == null && this.Right == null;
        }
    }
}
