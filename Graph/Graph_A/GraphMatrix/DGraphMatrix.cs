using Graph_A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public class DGraphMatrix<T> : AGraphMatrix<T>
        where T : IComparable<T>
    {
        public DGraphMatrix()
        {
            isDirected = true;
        }
        protected override Edge<T>[] GetAllEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();
            //Loop through the edge matrix
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] != null)
                    {
                        //Add the edges to the list
                        edges.Add(matrix[r, c]);
                    }
                }
            }
            return edges.ToArray();
        }

    }
}
