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
        public override Edge<T> GetEdge(T from, T to)
        {
            //There are 2 ways to get it
            //return matrix[GetVertex(from).Index, GetVertex(to).Index];
            return matrix[revLookUp[from], revLookUp[to]];
        }
        public override bool HasEdge(T from, T to)
        {
            return GetEdge(from, to) != null;
        }
        public override void RemoveEdge(T from, T to)
        {
            //If the edge does not exist, throw an exception
            if(!HasEdge(from, to))
            {
                throw new ApplicationException("Edge does not exist!");
            }
            //Assign the edge to null
            matrix[GetVertex(from).Index, GetVertex(to).Index] = null;
            //Decrement the edge count
            numEdges--;
        }
        public override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            //Reset the number of edges to 0
            numEdges = 0;
            //Create a reference to the old matrix
            Edge<T>[,] oldMatrix = matrix;
            //Create a new matrix with the current dimension
            matrix = new Edge<T>[NumVertices, NumVertices];
            //Add the edges from oldMatrix to new one
            for(int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for(int c = 0; c < oldMatrix.GetLength(2); c++)
                {
                    //If( an edge exists at the current location
                    if (oldMatrix[r, c] != null)
                    {
                        //This exclude the deleted row or column
                        if(r != v.Index && c != v.Index)
                        {
                            AddEdge(oldMatrix[r, c]);
                        }
                    }
                }
            }
        }
        public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            //Do some stuff to load up the list with neighbours of the data item passed in
            Vertex<T> vFrom = GetVertex(data);
            //Loop through the row for the vFrom in matrix
            for(int i = 0; i < matrix.GetLength(1); i++)
            {
                //If the current location in the vFrom row is not null
                //Add the corresponding edge to the list " neighbours"
                if (matrix[vFrom.Index, i] != null)
                {
                    //neighbours.Add(matrix[vFrom.Index, i].To);
                    neighbours.Add(vertices[i]);
                }
            }
            return neighbours;
        }
        public void TestEnumerateNeighbours(T data)
        {
            List<Vertex<T>> neighbours = (List<Vertex<T>>)EnumerateNeighbours(data);
            foreach(Vertex<T> v in neighbours)
            {
                Console.WriteLine(v);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\nEdge Matrix:\n");
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                Vertex<T> v = vertices[r];
                sb.Append(v.Data.ToString() + "\t");
                for(int c = 0; c < matrix.GetLength(1); c++)
                {
                    //{0, 12}: 12 characters, left align
                    sb.Append(String.Format("{0, 12}", matrix[r, c] == null ? "---- " : matrix[r, c].To.ToString()));
                }
                sb.AppendLine("\n");
                //sb.Append("\n");
            }
            //Return the vertices appended to the edges
            return base.ToString() + sb.ToString();
        }
    }
}
