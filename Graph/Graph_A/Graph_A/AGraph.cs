using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_A
{
    public abstract class AGraph<T>: IGraph<T>
        where T : IComparable<T>
    {
        #region Attributes
        //Store the vertices of the graph
        protected List<Vertex<T>> vertices;
        //A dictionary is hash table essentially
        //It supports genertic, We will use it to store
        //a data item index into the vertices list. This will
        //Make it much more efficient to look
        //a vertex in the vertices list
        protected Dictionary<T, int> revLookUp;
        //Stores the number of edges
        protected int numEdges;
        //Is the graph weighted
        protected bool isWeighted;
        //Is the graph directed;
        protected bool isDirected;
        #endregion

        #region Constructors
        public AGraph()
        {
            vertices = new List<Vertex<T>>();
            revLookUp = new Dictionary<T, int>();
            numEdges = 0;
        }
        #endregion

        #region IGraph Implementation
        public int NumVertices { get { return vertices.Count; } }
        public int NumEdges { get { return numEdges; } }
        #endregion

        #region Vertex methods
        public bool HasVertex(T data)
        {
            return revLookUp.ContainsKey(data);
        }
        public Vertex<T> GetVertex(T data)
        {
            if (!HasVertex(data))
            {
                throw new ApplicationException("Vertex does not exist!");
            }
            //Reverse lookup the index in the hastable (dictionary)
            //Note that C# overload the [] operator for its dictionary
            int index = revLookUp[data];
            return vertices[index];
        }
        public void AddVertex(T data)
        {
            if (HasVertex(data))
            {
                throw new ApplicationException("The " + data + "Vertex already exist!");
            }
            Vertex<T> vertex = new Vertex<T>(data, vertices.Count);
            vertices.Add(vertex);
            revLookUp.Add(data, vertex.Index);
            AddVertexAdjustEdges(vertex);
        }
        public void RemoveVertex(T data)
        {
            //If data is not in the vertices list, throw an exception
            if (!HasVertex(data))
            {
                throw new ApplicationException("The Vertex " + data + " does not exist!");
            }
            else //Data is in the list
            {
                //Get the vertex with the data
                Vertex<T> v = GetVertex(data);
                //Remove the vertex from the vertices list
                vertices.Remove(v);
                //Remove the data from the revLookup
                revLookUp.Remove(v.Data);
                //Decrement the indices of the vertices that were below the remvoed vertex
                for(int i = v.Index; i < vertices.Count; i++)
                {
                    //Decrement the vertex's index
                    vertices[i].Index--;
                    //Devrement the corresponding revLookup's data's index
                    revLookUp[vertices[i].Data]--;
                }
                //Tell the child class to remove any edges related to the removed vertex
                RemoveVertexAdjustEdges(v);
            }
        }
        #endregion

        #region Edge methods
        public virtual void AddEdge(T from, T to, double weight)
        {
            //If this is the 1st edge, then the graph will be unweighted
            if(numEdges == 0)
            {
                isWeighted = false;
            }
            else if (isWeighted)
            {
                throw new ApplicationException("Can't add unweighted edge!");
            }
            //Create a new Edge object and add it to the graph
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to));
            //Add the edge object to the graph
            AddEdge(e);
        }
        public virtual void AddEdge(T from, T to)
        {
            //If this is the 1st edge, then the graph will be unweighted
            if (numEdges == 0)
            {
                isWeighted = false;
            }
            else if (isWeighted)
            {
                throw new ApplicationException("Can't add unweighted edge!");
            }
            //Create a new Edge object and add it to the graph
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to));
            //Add the edge object to the graph
            AddEdge(e);
        }
        #endregion
        public IEnumerable<Vertex<T>> EnumerateVertices()
        {
            return vertices;
        }

        #region MST
        public IGraph<T> MinimumSpanningTree()
        {
            //Create an array of graph objects references
            AGraph<T>[] forest = new AGraph<T>[NumVertices];
            Edge<T>[] eArray = GetAllEdges();
            Array.Sort(eArray);
            /*List<Edge<T>> lEdges = GetAllEdges().ToList();
            lEdges.Sort(new EdgeComparer());*/
            int iTreeTo = 0;
            int iTreeFrom = 0;
            int iCurEdge = 0;
            for(int i = 0; i < NumVertices; i++)
            {
                //How to instantiate an instance of child in the parent class
                //Two methods:
                //1. Create an abstract method called CreateGraph()
                //2. Use C# reflection to return an instance of its child class
                AGraph<T> nGraph = (AGraph<T>)this.GetType().Assembly.CreateInstance(this.GetType().FullName);
                nGraph.AddVertex(vertices[i].Data);
                forest[i] = nGraph;
            }
            while(forest.Length > 1)
            {
                Edge<T> e = eArray[iCurEdge];
                iCurEdge++;
                iTreeFrom = FindTree(e.From, forest);
                iTreeTo = FindTree(e.To, forest);
                if(iTreeTo != iTreeFrom)
                {
                    forest[iTreeFrom].MergeTrees(forest[iCurEdge]);
                    forest[iTreeFrom].AddEdge(e.From.Data, e.To.Data, e.Weight);
                    forest = Timber(iTreeTo, forest);
                }
            }
            return forest[0];
        }
        private AGraph<T>[] Timber(int treeCut, AGraph<T>[] forest)
        {
            AGraph<T>[] result = new AGraph<T>[forest.Length-1];
            int j = 0;
            for(int i = 0; i < forest.Length; i++)
            {
                if(i != treeCut)
                {
                    result[j] = forest[i];
                    j++;
                }
            }
            return result;
        }
        private void MergeTrees(AGraph<T> treeTo)
        {
            foreach(Vertex<T> v in treeTo.EnumerateVertices())
            {
                this.AddVertex(v.Data);
            }
            foreach(Edge<T> e in treeTo.GetAllEdges())
            {
                this.AddEdge(e.From.Data, e.To.Data, e.Weight);
            }
        }
        private int FindTree(Vertex<T> objVertex, AGraph<T>[] forest)
        {
            int i = 0;
            //Check each graph until you find the target vertex
            while (forest[i].HasVertex(objVertex.Data) && i++ < forest.Length)
            {
                ;
            }
            //If the vertex is not in the forest
            if(i == forest.Length)
            {
                throw new ApplicationException("The graph is not found");
            }
            //Return its index
            return i;
        }
        private class EdgeComparer: IComparer<Edge<T>>
        {
            public int Compare(Edge<T> x, Edge<T> y)
            {
                return x.CompareTo(y);
            }
        }
        #endregion
        public void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //Get the starting vertex
            //Reference to the current vertex just popped off the stack
            //Create a dictionary to track the vertices already visited

            //Push the start vertex to the stack

            //While the stack has vertieces on it
            //Current vertex <-- pop item off stack
            //If the current vertex has not been visited
            //Process the current vertices data (WhatToDo)
            //Mark the current vertex as visited (add to dictionary)
            //Get a list of all neighbours of the current vertices
            //Foreach neighbour in the list
            //Add to the stack if it is visited or in is already on the stack

            Vertex<T> vStart = GetVertex(start);
            Vertex<T> vCurrent;
            Dictionary<T, T> visitedVertices = new Dictionary<T, T>();
            Stack<Vertex<T>> verticesRemaining = new Stack<Vertex<T>>();
            verticesRemaining.Push(vStart);
            while(verticesRemaining.Count > 0)
            {
                vCurrent = verticesRemaining.Pop();
                if (!visitedVertices.ContainsKey(vCurrent.Data))
                {
                    whatToDo(vCurrent.Data);
                    visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                    IEnumerable<Vertex<T>> vList = EnumerateNeighbours(vCurrent.Data);
                    foreach (Vertex<T> v in vList)
                    {
                        verticesRemaining.Push(v);
                    }
                }
            }
        }
        public void BreathFirstTraversal( T start, VisitorDelegate<T> whatToDo)
        {
            Vertex<T> vStart = GetVertex(start);
            Vertex<T> vCurrent;
            Dictionary<T, T> visitedVertices = new Dictionary<T, T>();
            Queue<Vertex<T>> verticesRemaining = new Queue<Vertex<T>>();
            verticesRemaining.Enqueue(vStart);
            while(verticesRemaining.Count > 0)
            {
                vCurrent = verticesRemaining.Dequeue();
                if (!visitedVertices.ContainsKey(vCurrent.Data))
                {
                    whatToDo(vCurrent.Data);
                    visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                    IEnumerable<Vertex<T>> vList = EnumerateNeighbours(vCurrent.Data);
                    foreach(Vertex<T> v in vList)
                    {
                        verticesRemaining.Enqueue(v);
                    }
                }
            }
        }

        #region Abstract methods
        protected abstract void AddEdge(Edge<T> e);
        public abstract void AddVertexAdjustEdges(Vertex<T> v);
        public abstract void RemoveVertexAdjustEdges(Vertex<T> v);
        protected abstract Edge<T>[] GetAllEdges(); //It depends on what kind of graph
        public abstract Edge<T> GetEdge(T from, T to);
        public abstract void RemoveEdge(T from, T to);
        public abstract bool HasEdge(T from, T to);
        public abstract IEnumerable<Vertex<T>> EnumerateNeighbours(T data);
        #endregion

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach(Vertex<T> v in EnumerateVertices())
            {
                result.Append(v + " ,");
            }
            if(vertices.Count > 0)
            {
                result.Remove(result.Length - 2, 2);
            }
            return this.GetType().Name + "\nVertices: " + result.ToString() +"\n";
        }
        public IGraph<T> ShortestWeightedPath(T start, T end)
        {
            throw new NotImplementedException();
        }
    }
}
