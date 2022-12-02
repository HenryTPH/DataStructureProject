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
        //Constructor to create an instance of Ugraph arraylist
        public UGraphAL()
        {
            //Set to non directed graph
            isDirected = false;
        }
        //Override number of edge by having half of them
        public override int NumEdges => base.NumEdges/2;
        //Override Add edge method which will add 2 way edge without having weight
        public override void AddEdge(T from, T to)
        {
            base.AddEdge(from, to);
            base.AddEdge(to, from);
        }
        //Override Add edge method which will add 2 way edge having weight
        public override void AddEdge(T from, T to, double weight)
        {
            base.AddEdge(from, to, weight);
            base.AddEdge(to, from, weight);
        }
        //Method to get all edges
        protected override Edge<T>[] GetAllEdges()
        {
            //Declare an instance of arraylist to store all the edges
            ArrayList edges = new ArrayList();
            //Loop through all the element in the vertex arrayList
            for(int i = 0; i < edgesArr.Count; i++)
            {
                //Cast the element at the i index as array list
                ArrayList eArr = (ArrayList)edgesArr[i];
                //Loop through all the element in the array list
                for(int j = 0; j < eArr.Count; j++)
                {
                    //Create an instance of edge at j index in the edge arraylist
                    Edge<T> edge = (Edge<T>)eArr[j];
                    //Assigne the index of To vertex of the edge to new variable
                    int toVertexIndex = edge.To.Index;
                    //if the To vertex's index is greater than i position
                    //Add to the edge array list
                    if(i < toVertexIndex)
                    {
                        edges.Add(edge);
                    }
                }
            }
            //Return the edge array 
            return (Edge<T>[])edges.ToArray(typeof(Edge<T>));
        }
    }
}
