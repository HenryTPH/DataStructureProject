using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_A
{
    public delegate void VisitorDelegate<T>(T value);
    public interface IGraph<T> where T : IComparable<T>
    {
        #region Properties
        int NumVertices { get; }
        int NumEdges { get; }
        #endregion

        #region Methods to work with vertices
        void AddVertex(T data);
        void RemoveVertex(T data);
        bool HasVertex(T data);
        Vertex<T> GetVertex(T data);
        #endregion

        #region Method to work with Edges
        void AddEdge(T from, T to);
        void AddEdge(T from, T to, double weight);
        bool HasEdge(T from, T to);
        Edge<T> GetEdge(T from, T to);
        void RemoveEdge(T from, T to);
        #endregion

        #region Methods that are essential graph algorithms
        void BreathFirstTraversal(T start, VisitorDelegate<T> whatToDo);
        void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo);
        IGraph<T> MinimumSpanningTree();
        IGraph<T> ShortestWeightedPath(T start, T end);
        #endregion
    }
}
