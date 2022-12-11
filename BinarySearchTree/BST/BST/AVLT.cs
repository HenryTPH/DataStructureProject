using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public class AVLT<T>: BinarySearchTree<T> where T: IComparable<T>
    {
        internal override Node<T> Balance(Node<T> current)
        {
            Node<T> newNode = current;
            if(current != null)
            {
                //Check the height difference between its two sub trees
                int iHeightDifference = GetHeightDifference(current);
                if(iHeightDifference > 1)
                {
                    //Check if its left child node's subtree height difference
                    int iLeftChildHeightDifference = GetHeightDifference(current.Left);
                    //If the left child subtree is right heavy
                    if (iLeftChildHeightDifference < 0)
                    {
                        newNode = DoubleRight(current);
                    }
                    else
                    {
                        newNode = SingleLeft(current);
                    }
                }
                if(iHeightDifference < -1)
                {
                    int iRightChildHeightDifference = GetHeightDifferenct(current.Right);
                    if(iRightChildHeightDifference > 0)
                    {
                        newNode = DoubleLeft(current);
                    }
                    else
                    {
                        newNode = SingleRight(current);
                    }
                }
            }
            return newNode;
        }
        private Node<T> SingleLeft(Node<T> nOldRoot)
        {
            Node<T> newNode = nOldRoot.Right;
            nOldRoot.Right = newNode.Left;
            newNode.Left = nOldRoot;
            return newNode;
        }
        private Node<T> SingleRight(Node<T> nOldRoot)
        {
            Node<T> newNode = nOldRoot.Left;
            nOldRoot.Left = newNode.Right;
            newNode.Right = nOldRoot;
            return newNode;
        }
        private Node<T> DoubleLeft(Node<T> nOldRoot)
        {
            nRoot.Right = SingleRight(nOldRoot.Right);
            return SingleLeft(nOldRoot);
        }
        private Node<T> DoubleRight(Node<T> nOldRoot)
        {
            nRoot.Left = SingleLeft(nOldRoot.Left);
            return SingleRight(nOldRoot);
        }
        private int GetHeightDifference(Node<T> current)
        {
            int iHeightLeft = -1;
            int iHeightRight = -1;
            if(current.Left != null)
            {
                iHeightLeft = RecHeight(current.Left);
            }
            if(current.Right != null)
            {
                iHeightRight= RecHeight(current.Right);
            }
            return iHeightLeft - iHeightRight;
        }
        public void TestHeightDifference()
        {
            if(nRoot != null)
            {
                Console.WriteLine("Height Difference from the root of AVLT is: " + GetHeightDifference(nRoot));
            }
        }
        //Iterative method to add element to AVLT and balance it
        public override void Add(T data)
        {
            if(nRoot == null)
            {
                nRoot= new Node<T>(data);
            }
            //If the root of AVL tree is not null
            else
            {
                //Create an instance of a node
                Node<T> current = new Node<T>();
                //Create a stack to contain the node
                Stack<Node<T>> stack = new Stack<Node<T>>();
                int nCount = 0; //Counting the loop
                current = nRoot; //Assign root to current node
                //While nCount is less than AVLT count
                while(nCount <= this.Count)
                {
                    //Push the current element to the stack
                    stack.Push(current);
                    //If current data is larger than data
                    //Move to the left of the AVL tree
                    if(current.Data.CompareTo(data) > 0)
                    {
                        //If left node of current is null
                        //Add new node to the left of current and break
                        //else assign current is current left node
                        if(current.Left != null)
                        {
                            current.Left = new Node<T>(data);
                            break;
                        }
                        else
                        {
                            current = current.Left;
                        }
                        nCount++; //Increase the loop count
                    }
                    //If current data is less than data
                    //Move to the right of the AVL tree
                    else
                    {
                        //If right node of current is null
                        //Add new node to the right of current and break
                        //else assign current is current right node
                        if (current.Right != null)
                        {
                            current.Right = new Node<T>(data);
                            break;
                        }
                        else
                        {
                            current = current.Right;
                        }
                        nCount++; //Increase the loop count
                    }
                }
                //Increase the count of AVL tree
                this.iCount++;

                // ============ Balance the AVL tree ================
                //Store the number of element of stack
                int stackCount = stack.Count;
                //If stack is not empty
                if(stackCount > 0 )
                {
                    //Create 3 instance of node
                    Node<T> currentNode;
                    Node<T> next;
                    Node<T> temp;
                    //Pop out from stack as current node
                    currentNode = stack.Pop();
                    for(int i = 0; i < stackCount; i++)
                    {
                        //Balance the node instance and assign it to temp node
                        temp = Balance(currentNode);
                        //If the node is the root, assign temp node as new root of AVL tree
                        if(currentNode == nRoot)
                        {
                            next = null;
                            nRoot = temp;                            
                            break;
                        }
                        //else, pop out a new node assing it as next node
                        else
                        {
                            //If the unbalance node is the right node of the next node
                            //Assign the balance node - temp node - to the right node of the next node
                            //If the unbalance node is the left of the next node
                            //Assign the balance node - temp node - to the left node of the next node
                            next = stack.Pop();
                            if(next.Right == currentNode)
                            {
                                next.Right = temp;
                            }
                            else
                            {
                                next.Left = temp;
                            }
                            //Assign next node to the node
                            currentNode = next;
                        }
                    }
                }

                //Another solution to balance AVLT
               /* Node<T> temp1;
                while (stack.Count > 0)
                {
                    temp1 = stack.Pop();
                    temp1 = Balance(temp1);
                    if(stack.Count == 0)
                    {
                        nRoot = temp1;
                    }
                }*/
            }
        }
        //Iterative Pre-Order, Post-Order, and In-Order
        public override void iterate(ProcessData<T> pd, TRAVERSALORDER to)
        {
            //If the traversal order is Pre-Order
            if(to == TRAVERSALORDER.PRE_ORDER)
            {
                //Stack contains nodes
                Stack<Node<T>> stack = new Stack<Node<T>>();
                //If the root of AVLT is not null
                if(nRoot != null)
                {
                    //Push it to the stack
                    stack.Push(nRoot);
                    //While stack is not empty
                    while(stack.Count > 0)
                    {
                        Node<T> node = stack.Pop();
                        pd(node.Data);
                        if(node.Right != null)
                        {
                            stack.Push(node.Right);
                        }
                        if(node.Left != null)
                        {
                            stack.Push(node.Left);
                        }
                    }
                }
            }
            if(to == TRAVERSALORDER.POST_ORDER)
            {
                if(nRoot != null)
                {
                    Stack<Node<T>> tempContainer = new Stack<Node<T>>();
                    Stack<Node<T>> resultContainer = new Stack<Node<T>>();
                    tempContainer.Push(nRoot);
                    while(tempContainer.Count > 0)
                    {
                        Node<T> node = tempContainer.Pop();
                        resultContainer.Push(node);
                        if(node.Left != null)
                        {
                            tempContainer.Push(node.Left);
                        }
                        if(node.Right != null)
                        {
                            tempContainer.Push(node.Right);
                        }
                    }
                    while(resultContainer.Count > 0)
                    {
                        T data = resultContainer.Pop().Data;
                        pd(data);
                    }
                }
            }
            if(to == TRAVERSALORDER.IN_ORDER)
            {
                Node<T> checkNode = null;
                if(nRoot != null)
                {
                    Stack<Node<T>> stack = new Stack<Node<T>>();
                    checkNode = nRoot;
                    int nodeCount = 0;
                    while(checkNode!= null)
                    {
                        stack.Push(checkNode);
                        checkNode = checkNode.Left;
                        while(checkNode == null && nodeCount <= this.Count)
                        {
                            checkNode = stack.Pop();
                            pd(checkNode.Data);
                            nodeCount++;
                            checkNode = checkNode.Right;
                        }
                    }
                }
            }
        }
    }
}
