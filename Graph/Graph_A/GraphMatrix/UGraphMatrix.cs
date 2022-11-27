using Graph_A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public class UGraphMatrix<T>: AGraphMatrix<T>
        where T : IComparable<T>
    {
        public UGraphMatrix()
        {
            isDirected = false;
        }
        //Because we add 2 way in undirected graph, so we have to divide by 2
        public override int NumEdges { get { return base.numEdges/2; } }
        public override void AddEdge(T from, T to)
        {
            base.AddEdge(from, to);
            base.AddEdge(to, from);
        }
        public override void AddEdge(T from, T to, double weight)
        {
            base.AddEdge(from, to, weight);
            base.AddEdge(to, from, weight);
        }
        protected override Edge<T>[] GetAllEdges()
        {
            //Create a new empty list of edges
            List<Edge<T>> edges = new List<Edge<T>>();
            for(int r = 0; r < matrix.GetLength(0); r++)
            {
                for(int c = 0; c < r; c++)
                {
                    if (matrix[r, c] != null)
                    {
                        edges.Add(matrix[r, c]);
                    }
                }
            }
            return edges.ToArray();
        }
    }
}
