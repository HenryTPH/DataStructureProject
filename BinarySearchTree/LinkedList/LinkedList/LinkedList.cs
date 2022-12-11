using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LinkedList
{
    public class LinkedList<T> : A_List<T> where T : IComparable<T>
    {
        #region Attributes
        private Node head;
        #endregion
        public override void Add(T data)
        {
            //throw new NotImplementedException();
            head = RecAdd(head, data);
        }
        //Recursive helper for Add method
        private Node RecAdd(Node current, T data)
        {
            if (current == null)
            {
                current = new Node(data);
            }
            else
            {
                current.next = RecAdd(current.next, data);
            }
            return current;
        }

        /*public override void Add(T data)
        {
            if(head == null)
            {
                head = new Node(data);
            }
            else
            {
                RecAdd2(head, data);
            }
        }

        private void RecAdd2(Node current, T data)
        {
            if(current.next == null)
            {
                current.next = new Node(data);
            }
            else
            {
                RecAdd2(current.next, data);
            }
        }*/

        public override void Insert(int index, T data)
        {
            //Check if index is out of range, if yes, throw an exception
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                Node newNode = new Node(data, head);
                head = newNode;
            }
            else
            {
                RecInsert2(index, head, data);
            }
        }

        private void RecInsert2(int index, Node current, T data)
        {
            if (index == 1)
            {
                Node newNode = new Node(data, current.next);
                current.next = newNode;
            }
            else
            {
                RecInsert2(--index, current.next, data);
            }
        }

        /*public override void Insert(int index, T data)
        {
            //throw new NotImplementedException();
            //Check if index is out of range, if yes throw an exception

            //Use recursion to insert your data at the index
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                head = RecInsert(head, index, data);
            }
        }

        private Node RecInsert(Node current, int index, T data)
        {
            if (index == 0)
            {
                current = new Node(data, current);
            }
            else
            {
                current.next = RecInsert(current.next, --index, data);
            }
            return current;
        }*/

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }
        //Question 6: Complete the RemoveAt function
        public override T RemoveAt(int index)
        {
            //If head is null or index is less than 0 or index is greater or equal to number of elements in linked list
            //Throw an exception
            if (head == null || index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("The list is empty");
            }
            //If the index is 0, then assign the head to the next element of the old head
            //Else call function to recursively remove at that index
            if(index == 0)
            {
                T result = head.data;
                head = head.next;
                return result;
            }
            else
            {
                return RmAtIndex(index, ref head); // We need the ref here based on George solution
            }
        }
        //Recursive method to remove an element at specific index
        private T RmAtIndex(int index, ref Node current)
        {
            //Create a variable named result with generic type T
            T result;
            //If index is 1, assign the data of the next node of current node to the result
            //assign the next current as the next of this current
            //else call recursive method to remove at new index which is less than old index 1 value for the next node of current
            if(index == 1)
            {
                result = current.next.data;
                current.next = current.next.next;
            }
            else
            {
                result = RmAtIndex(--index, current.next);
            }
            return result;
        }
        //Question 6: Complete the ReplaceAt function
        public override T ReplaceAt(int index, T data)
        {
            //If head is null or index is less than 0 or index is greater or equal to number of elements in linked list
            //Throw an exception
            if (index < 0 || index >= this.Count || head == null)
            {
                throw new IndexOutOfRangeException("Index is out of range or list is null");
            }
            //If the index is 0, then assign the head's data to the new data
            //Else call function to recursively replace at that index with specific data
            if (index == 0)
            {
                T result = head.data;
                head.data = data;
                return result;
            }
            else
            {
                return RecReplaceAt(index, head, data);
            }
        }
        //Recursive method to replace an element at specific index with particular data
        private T RecReplaceAt(int index, Node current, T data)
        {
            //Create a variable named result with generic type T
            T result;
            //If index is 1, assign the new data to the next node of current
            //else call recursive method to replace at new index which is less than old index 1 value for the next node of current
            if (index == 1)
            {
                result = current.next.data;
                current.next.data = data;
            }
            else
            {
                result = RecReplaceAt(--index, current.next, data);
            }
            return result;
        }

        public override bool Remove(T data)
        {
            //throw new NotImplementedException();
            return RecRemove(ref head, data); //Using ref must use variable not a value
        }
        //Ref indicates that a value is passed by reference
        private bool RecRemove(ref Node current, T data)
        {
            bool found = false;
            if(current != null)
            {
                if(current.data.CompareTo(data) == 0)
                {
                    found = true;
                    current = current.next;
                }
                else
                {
                    found = RecRemove(ref current.next, data);
                }
            }
            return found;
        }

        #region Enumerator implementation
        private class Enumerator : IEnumerator<T>
        {
            //Reference to the linked list
            private LinkedList<T> parent;
            //Reference to current node being visited
            private Node lastVisited;
            //The next node that we want to visit
            private Node scout;

            //Constructor
            public Enumerator(LinkedList<T> parent)
            {
                this.parent = parent;
                Reset();
            }

            public T Current
            {
                get
                {
                    return lastVisited.data;
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return lastVisited;
                }
            }

            //Clear up the resources used
            //Set references to null
            //Clean when you are done with the inumerator
            public void Dispose()
            {
                parent = null;
                scout = null;
                lastVisited = null;
            }

            public bool MoveNext()
            {
                bool result = false;
                if (scout != null)
                {
                    //We can definitely move
                    result = true;
                    //Move current node pointer to the next node
                    lastVisited = scout;
                    //Move scout to the next node
                    scout = scout.next;
                }
                return result;
            }

            public void Reset()
            {
                lastVisited = null;
                scout = parent.head;
            }
        }
        #endregion
        private class Node
        {
            #region Attributes
            public T data;
            public Node next;
            #endregion
            //Constructors
            public Node(T data) : this(data, null) { }
            public Node(T data, Node next)
            {
                this.data = data;
                this.next = next;
            }

        }
    }
}
