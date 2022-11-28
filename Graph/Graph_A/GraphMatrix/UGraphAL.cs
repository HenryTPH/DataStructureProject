using Graph_A;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public class UGraphAL<T>: AAdjacencyList<T>
        where T : IComparable<T>
    {
        public UGraphAL()
        {
            isDirected = false;
        }
        public override int NumEdges => base.NumEdges/2;
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
            ArrayList edges = new ArrayList();
            for(int i = 0; i < edgesArr.Count; i++)
            {
                ArrayList eArr = (ArrayList)edgesArr[i];
                for(int j = 0; j < eArr.Count; j++)
                {
                    Edge<T> edge = (Edge<T>)eArr[j];
                    int toVertexIndex = edge.To.Index;
                    if(i < toVertexIndex)
                    {
                        edges.Add(edge);
                    }
                }
            }
            return (Edge<T>[])edges.ToArray(typeof(Edge<T>));
        }
    }
}
