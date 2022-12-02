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
        //Declare an instance of arraylist to store vertices and edges
        protected ArrayList edgesArr = new ArrayList();
        //Constructor will generate a AAdjacencyList in which each element in edgesArr
        //will be an instance of array list
        public AAdjacencyList()
        {
            for(int i= 0; i < vertices.Count; i++)
                {
                    ArrayList eArr = new ArrayList();
                    edgesArr.Add(eArr);
                } 
        }
        //Method will add an edge to the arraylist
        protected override void AddEdge(Edge<T> e)
        {
            //If there has that edge in array list
            //Throw an exception
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists!");
            }
            //Cast the element at the From vertex data's index as array list
            ArrayList vertexPos = (ArrayList)edgesArr[e.From.Index];
            //Add the edge to the above array list
            vertexPos.Add(e);
            //Increase the number of edges
            numEdges++;
        }
        //Method will add and vertex to the arraylist and adjust edges
        public override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //Create a new instance of arraylist and add into the arraylist of edges
            ArrayList eArr = new ArrayList();
            edgesArr.Add(eArr);
        }
        //Method will get an edge having from-to vertex
        public override Edge<T> GetEdge(T from, T to)
        {
            //Cast the element at the From vertex data's index as array list
            ArrayList eArr = (ArrayList)edgesArr[GetVertex(from).Index];
            //Enumerate each element in the array list
            //Find the edge that have the equal From vertex data and To vertex data
            //Then return it. Otherwise, return null
            foreach(Edge<T> e in eArr)
            {
                if(e.From.Data.Equals(from) && e.To.Data.Equals(to))
                {
                    return e;
                }
            }
            return null;
        }
        //Method will check if there is an edge
        public override bool HasEdge(T from, T to)
        {
            //Cast the element at the From vertex data's index as array list
            ArrayList eArray = (ArrayList)edgesArr[GetVertex(from).Index];
            //Declare an instance of edge having a From vertex and To vertex
            Edge<T> edge = new Edge<T>(GetVertex(from), GetVertex(to));
            //Return the result as a boolean to check if the edge is in the array list or not
            return eArray.Contains(edge);
        }
        //Method to remove an edge
        public override void RemoveEdge(T from, T to)
        {
            //If edge does not exist, throw an exception            
            if(!HasEdge(from, to))
            {
                throw new ApplicationException("Edge does not exist!");
            }
            //Cast the element at the From vertex data's index as array list
            ArrayList eArray = (ArrayList)edgesArr[GetVertex(from).Index];
            //Declare an instance of edge having a From vertex and To vertex
            Edge<T> edge = new Edge<T>(GetVertex(from), GetVertex(to));
            //Remove the edge from the array
            eArray.Remove(edge);
            //Assign the array to the object at From Vertex index
            edgesArr[GetVertex(from).Index] = eArray;
            //Decrease the number of edges
            numEdges--;            
        }
        //Method will remove vertex and adjust the edges
        public override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            //Declare an index variable receiving the index of vertex
            int vIndex = v.Index;
            //Remove the vertex from from the arraylist at that index
            edgesArr.RemoveAt(vIndex);
            //Enumerate each edge-arrayList element in vertex arraylist
            foreach(ArrayList eArr in edgesArr)
            {
                //Loop through all the element of the edge arraylist
                for(int i = 0; i < eArr.Count; i++)
                {
                    //Assign the edge element at location i to a new edge instance
                    Edge<T> e = (Edge<T>)eArr[i];
                    //If the edge has the To vertex data is equal to the vertex data
                    //also remove that edge from the edge arraylist
                    if (e.To.Data.Equals(v.Data))
                    {
                        eArr.Remove(e);
                    }
                }
            }
        }
        //Method will enumerate all neighbours of a vertex data
        public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
        {
            //Create a list instance to store the list of vertex
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            //Cast the element at the vertex data's index as array list
            ArrayList eArr = (ArrayList)edgesArr[GetVertex(data).Index];
            //Enumerate all element in the arraylist to add all To vertex to the neighbour list
            foreach(Edge<T> e in eArr)
            {
                neighbours.Add(e.To);
            }
            //Return the neighbours list
            return neighbours;
        }
        //Test method will try to print out all the neighbour of a vertex data
        public void TestEnumerateNeighbours(T data)
        {
            //Create an arraylist of neighbours of data generated from EnumerateNeighbours method
            ArrayList neighbours = (ArrayList)EnumerateNeighbours(data);
            //Enumerate all the elements in neighbours arraylist and print it out
            foreach(Vertex<T> e in neighbours)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //Method to print out all the elements of the graph
        public override string ToString()
        {
            //Declare a string builder instance
            StringBuilder sb = new StringBuilder("\nEdgeArray:\n");
            //Loop through all the edge arraylist from vertex arraylist
            for(int i = 0; i < edgesArr.Count; i++)
            {
                //Get the vertex at i position and append to the string builder object
                Vertex<T> v = vertices[i];
                sb.Append(v.Data.ToString() + "\t");
                //Cast the object at i position as an arraylist
                ArrayList eArr = (ArrayList)edgesArr[i];
                //Enumerate all elemetn in the array list and append the element to the string builder object
                foreach(Edge<T> e in eArr)
                {
                    sb.Append(String.Format("{0, 12}", e.ToString() + ",\t"));
                }
                sb.Append("\n");
            }
            //Return the string of string builder object
            return base.ToString() + sb.ToString();
        }
    }
}
