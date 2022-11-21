using Graph_A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public abstract class AGraphMatrix<T>: AGraph<T>
        where T : IComparable<T>
    {
        protected Edge<T>[,] matrix;
        public AGraphMatrix()
        {
            matrix = new Edge<T>[0, 0];
        }
        protected override void AddEdge(Edge<T> e)
        {
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists!");
            }
            //Index to the edge matrix and add the edge
            matrix[e.From.Index, e.To.Index] = e;
            //Increment the number of edges
            numEdges++;
        }
        public override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //Create a reference to old matrix
            Edge<T>[,] oldMatrix = matrix;
            //Create the larger matrix
            matrix = new Edge<T>[NumVertices, NumVertices];
            for(int i = 0; i < oldMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < oldMatrix.GetLength(1); j++)
                {
                    matrix[i, j] = oldMatrix[i, j];
                }
            }
        }
    }
}
