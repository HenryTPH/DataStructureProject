using Graph_A;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public abstract class AAdjacencyList<T>: AGraph<T>
        where T : IComparable<T>
    {
        protected ArrayList edgesArr = new ArrayList();
        public AAdjacencyList()
        {
            for(int i= 0; i < vertices.Count; i++)
                {
                    ArrayList eArr = new ArrayList();
                    edgesArr.Add(eArr);
                } 
        }
        protected override void AddEdge(Edge<T> e)
        {
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists!");
            }
            ArrayList vertexPos = (ArrayList)edgesArr[e.From.Index];
            vertexPos.Add(e);
            numEdges++;
        }
        public override void AddVertexAdjustEdges(Vertex<T> v)
        {
            ArrayList eArr = new ArrayList();
            edgesArr.Add(eArr);
        }
        public override Edge<T> GetEdge(T from, T to)
        {
            ArrayList eArr = (ArrayList)edgesArr[GetVertex(from).Index];
            foreach(Edge<T> e in eArr)
            {
                if(e.From.Data.Equals(from) && e.To.Data.Equals(to))
                {
                    return e;
                }
            }
            return null;
        }
        public override bool HasEdge(T from, T to)
        {
            ArrayList eArray = (ArrayList)edgesArr[GetVertex(from).Index];
            Edge<T> edge = new Edge<T>(GetVertex(from), GetVertex(to));
            return eArray.Contains(edge); // RECHECK KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
        }
        public override void RemoveEdge(T from, T to)
        {
            //If edge does not exist, throw an exception
            if(!HasEdge(from, to))
            {
                throw new ApplicationException("Edge does not exist!");
            }
            ArrayList eArray = (ArrayList)edgesArr[GetVertex(from).Index];
            Edge<T> edge = new Edge<T>(GetVertex(from), GetVertex(to));
            eArray.Remove(edge);
            edgesArr[GetVertex(from).Index] = eArray;
            numEdges--;
        }
        public override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            int vIndex = v.Index;
            edgesArr.RemoveAt(vIndex);
            foreach(ArrayList eArr in edgesArr)
            {
                foreach(Edge<T> e in eArr)
                {
                    if (e.To.Data.Equals(v.Data))
                    {
                        eArr.Remove(e);
                    }
                }

            }
        }
        public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            ArrayList eArr = (ArrayList)edgesArr[GetVertex(data).Index];
            foreach(Edge<T> e in eArr)
            {
                neighbours.Add(e.To);
            }
            return neighbours;
        }
        public void TestEnumerateNeighbours(T data)
        {
            ArrayList neighbours = (ArrayList)EnumerateNeighbours(data);
            foreach(Vertex<T> e in neighbours)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\nEdgeMatrix:\n");
            for(int i = 0; i < edgesArr.Count; i++)
            {
                Vertex<T> v = vertices[i];
                sb.Append(v.Data.ToString() + "\t");
                ArrayList eArr = (ArrayList)edgesArr[i];
                foreach(Edge<T> e in eArr)
                {
                    sb.Append(String.Format("{0, 12}", e.ToString() + ",\t"));
                }
                sb.Append("\n");
            }
            return base.ToString() + sb.ToString();
        }
    }
}
