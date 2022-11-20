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
        int NumVertieces { get; }
        int NumEdges { get; }
        #endregion

        #region Methods to work with vertices
        void AddVertex(T data);
        void RemoveVertex(T data);
        bool HasVertex(T data);
        Vertex<T> GetVertex(T data);
        #endregion
    }
}
