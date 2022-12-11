using LinkedListPractice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BST
{
    public class BinarySearchTree<T> : A_BST<T>, ICloneable where T : IComparable<T>
    {
        public BinarySearchTree()
        {
            nRoot = null;
            iCount= 0;
        }
        public T FindSmallest()
        {
            if (nRoot == null)
            {
                throw new ApplicationException("Root is null");
            }
            else
            {
                return RecFindSmallest(nRoot);
            }
        }
        private T RecFindSmallest(Node<T> current)
        {
            if (current.Left == null)
            {
                return current.Data;
            }
            else
            {
                return RecFindSmallest(current.Left);
            }
        }
        public T FindLargest()
        {
            if (nRoot == null)
            {
                throw new ApplicationException("Root is null");
            }
            else
            {
                return RecFindLargest(nRoot);
            }
        }
        private T RecFindLargest(Node<T> current)
        {
            if (current.Right == null)
            {
                return current.Data;
            }
            else
            {
                return RecFindLargest(current.Right);
            }
        }
        public object Clone()
        {
            //Create a new instance of BST
            BinarySearchTree<T> bst = new BinarySearchTree<T>();
            //Set the root to the clone of this root recursively
            bst.nRoot = RecClone(nRoot);
            //Set the count to this BST's count
            bst.iCount= iCount;
            //Return the newly cloned BST
            return (Object)bst;
        }
        private Node<T> RecClone(Node<T> current)
        {
            //Create a new Node reference
            Node<T> newNode = null;
            //If current is not null
            if (current!= null)
            {
                newNode = new Node<T>(current.Data);
                newNode.Left = RecClone(current.Left);
                newNode.Right= RecClone(current.Right);
            }
            return newNode;
        }
        public override T Find(T data)
        {
            T tFound = default(T);
            tFound = RecFind(data, nRoot);
            return tFound;
        }
        private T RecFind(T data, Node<T> current)
        {
            if (current == null)
            {
                return default(T);
            }
            else
            {
                int iCompare = current.Data.CompareTo(data);
                if (iCompare == 0)
                {
                    return current.Data;
                }
                else if (iCompare > 0)
                {
                    return RecFind(data, current.Left);
                }
                else
                {
                    return RecFind(data, current.Right);
                }
            }
        }
        public override int Height()
        {
            int iHeight = -1;
            if (nRoot != null)
            {
                iHeight = RecHeight(nRoot);
            }
            return iHeight;
        }
        protected int RecHeight(Node<T> current)
        {
            int iHeightRight = 0;
            int iHeightLeft = 0;
            if (!current.IsLeaf())
            {
                if(current.Left != null)
                {
                    iHeightLeft= RecHeight(current.Left) + 1;
                }
                if(current.Right!= null)
                {
                    iHeightRight= RecHeight(current.Right) + 1;
                }
            }
            return iHeightRight > iHeightLeft? iHeightRight: iHeightLeft;
        }
        public override void iterate(ProcessData<T> pd, TRAVERSALORDER to)
        {
            if(nRoot!= null)
            {
                RecIterate(nRoot, pd, to);
            }
        }
        private void RecIterate(Node<T> current, ProcessData<T> pd, TRAVERSALORDER to)
        {
            if(to == TRAVERSALORDER.PRE_ORDER)
            {
                pd(current.Data);
            }
            if(current.Left != null)
            {
                RecIterate(current.Left, pd, to);
            }
            if(to == TRAVERSALORDER.IN_ORDER)
            {
                pd(current.Data);
            }
            if(current.Right != null)
            {
                RecIterate(current.Right, pd, to);
            }
            if(to == TRAVERSALORDER.POST_ORDER)
            {
                pd(current.Data);
            }
        }
        public override void Add(T data)
        {
            if(nRoot == null)
            {
                nRoot = new Node<T>(data);
            }
            else
            {
                RecAdd(nRoot, data);
                nRoot = this.Balance(nRoot);
            }
            iCount++;
        }
        private void RecAdd(Node<T> current, T data)
        {
            int iResult =  data.CompareTo(current.Data);
            if(iResult < 0)
            {
                if(current.Left == null)
                {
                    current.Left = new Node<T>(data);
                }
                else
                {
                    RecAdd(current.Left, data);
                    current.Left = Balance(current.Left);
                }
            }
            else
            {
                if(current.Right == null)
                {
                    current.Right = new Node<T>(data);
                }
                else
                {
                    RecAdd(current.Right, data);
                    current.Right = Balance(current.Right);
                }
            }
        }
        internal virtual Node<T> Balance(Node<T> node)
        {
            return node;
        }
        public override void Clear()
        {
            nRoot= null;
            iCount= 0;
        }
        public override bool Remove(T data)
        {
            bool bRemoved = false;
            nRoot = RecRemove(nRoot, data, ref bRemoved);
            return bRemoved;
        }
        private Node<T> RecRemove(Node<T> current, T data, ref bool bRemoved)
        {
            T temp = default;
            int iCompare = 0;
            if(current!= null)
            {
                iCompare = data.CompareTo(current.Data);
                if(iCompare < 0)
                {
                    current.Left = RecRemove(current.Left, data, ref bRemoved);
                }
                else if(iCompare > 0)
                {
                    current.Right = RecRemove(current.Right, data, ref bRemoved);
                }
                else
                {
                    bRemoved = true;
                    if (current.IsLeaf())
                    {
                        iCount--;
                        current = null;
                    }
                    else if(current.Left!= null && current.Right == null)
                    {
                        current = current.Left;
                        iCount--;
                    }
                    else if(current.Right != null && current.Left== null)
                    {
                        current = current.Right;
                        iCount--;
                    }
                    else
                    {
                        temp = RecFindLargest(current.Left);
                        current.Data = temp;
                        current.Left = RecRemove(current.Left, temp, ref bRemoved);
                    }
                }
            }
            return current;
        }
        public override IEnumerator<T> GetEnumerator()
        {
            return new BreadthFirstEnumerator(this);
        }
        private class BreadthFirstEnumerator: IEnumerator<T>
        {
            private BinarySearchTree<T> parent = null;
            private Node<T> current = null;
            private Queue<Node<T>> sNodes;
            public BreadthFirstEnumerator(BinarySearchTree<T> parent)
            {
                this.parent = parent;
                Reset();
            }
            public T Current => current.Data;
            object IEnumerator.Current => current.Data;
            public void Dispose()
            {
                parent = null;
                current= null;
                sNodes = null;
            }
            public bool MoveNext()
            {
                bool bMove = false;
                if(sNodes.Count > 0)
                {
                    bMove= true;
                    current = sNodes.Dequeue();
                    if(current.Left != null)
                    {
                        sNodes.Enqueue(current.Left);
                    }
                    if(current.Right != null)
                    {
                        sNodes.Enqueue(current.Right);
                    }
                }
                return bMove;
            }
            public void Reset()
            {
                sNodes = new Queue<Node<T>>();
                if(parent.nRoot != null)
                {
                    sNodes.Enqueue(parent.nRoot);
                }
                current = null;
            }
        }
        private class DepthFirstEnumerator: IEnumerator<T>
        {
            private BinarySearchTree<T> parent;
            private Node<T> current;
            private Stack<Node<T>> sNodes;
            public DepthFirstEnumerator(BinarySearchTree<T> parent)
            {
                this.parent = parent;
                Reset();
            }
            public T Current => this.Current;
            object IEnumerator.Current => this.Current;
            public void Dispose()
            {
                parent = null;
                current = null;
                sNodes = null;
            }
            public bool MoveNext()
            {
                bool bMove = false;
                if(sNodes.Count > 0)
                {
                    bMove = true;
                    current = sNodes.Pop();
                    if(current.Right != null)
                    {
                        sNodes.Push(current.Right);
                    }
                    if(current.Left != null)
                    {
                        sNodes.Push(current.Left);
                    }
                }
                return bMove;
            }
            public void Reset()
            {
                sNodes = new Stack<Node<T>>();
                if(parent.nRoot != null)
                {
                    sNodes.Push(parent.nRoot);
                }
                current= null;
            }
        }
        public LinkedList.LinkedList<T> toLinkedList()
        {
            //Create a null linkedlist
            LinkedList.LinkedList<T> list = null;
            if(nRoot!= null)
            {
                list = new LinkedList.LinkedList<T>();
                RecToLinkedList(nRoot,  list);
            }
            return list;
        }
        private void RecToLinkedList(Node<T> current, LinkedList.LinkedList<T> list)
        {
            //First solution - descending order
            BinarySearchTree<T> cloneBST = (BinarySearchTree<T>)Clone();
            while(cloneBST.Count > 0)
            {
                T data = cloneBST.FindLargest();
                list.Add(data);
                cloneBST.Remove(data);
            }

            //Second solution
            if(current.Right != null)
            {
                RecToLinkedList(current.Right, list);
            }
            list.Add(current.Data);
            if(current.Left != null)
            {
                RecToLinkedList(current.Left, list);
            }
        }
    }
}
