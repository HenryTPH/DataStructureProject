using Graph_A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public class Program
    {
        static void AddData(IGraph<string> g)
        {
            g.AddVertex("SA");
            g.AddVertex("PA");
            g.AddVertex("REG");
            g.AddVertex("MJ");
            g.AddVertex("WYN");
            g.AddVertex("YK");
            g.AddVertex("SC");
            g.AddVertex("WEY");

            g.AddEdge("SA", "PA", 140);
            g.AddEdge("SA", "REG", 250);
            g.AddEdge("SA", "SC", 300);
            g.AddEdge("MJ", "REG", 72);
            g.AddEdge("MJ", "SC", 200);
            g.AddEdge("SA", "WYN", 189);
            g.AddEdge("WYN", "YK", 140);
            g.AddEdge("REG", "YK", 190);
            g.AddEdge("WEY", "REG", 116);
        }
        static void DisplayData(string data)
        {
            Console.WriteLine(data);
        }
        static void TestDepthFirstTraversal(string start, IGraph<string> g)
        {
            g.DepthFirstTraversal(start, DisplayData);
        }
        static void TestBreadFirstTraversal(string start, IGraph<string> g)
        {
            g.BreathFirstTraversal(start, DisplayData);
        }

        static void TestMST()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            try
            {
                uGraph.AddVertex("A");
                uGraph.AddVertex("B");
                uGraph.AddVertex("C");
                uGraph.AddVertex("D");
                uGraph.AddVertex("E");
                uGraph.AddVertex("F");
                uGraph.AddVertex("G");
                uGraph.AddVertex("H");

                uGraph.AddEdge("A", "F", 10);
                uGraph.AddEdge("A", "B", 28);
                uGraph.AddEdge("B", "H", 13);
                uGraph.AddEdge("F", "E", 25);
                uGraph.AddEdge("B", "G", 14);
                uGraph.AddEdge("B", "C", 16);
                uGraph.AddEdge("E", "G", 24);
                uGraph.AddEdge("E", "D", 22);
                uGraph.AddEdge("G", "D", 18);
                uGraph.AddEdge("D", "C", 12);

                UGraphMatrix<string> minSpanningTree = (UGraphMatrix<string>)uGraph.MinimumSpanningTree();
                Console.WriteLine("======== Test Minimum Spanning Tree for Matrix Graph ============");
                Console.WriteLine(minSpanningTree.ToString());
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Exception testing MST " + ex);
            }
        }
        static void TestMSTAL()
        {
            UGraphAL<string> uGraph = new UGraphAL<string>();
            try
            {
                uGraph.AddVertex("A");
                uGraph.AddVertex("B");
                uGraph.AddVertex("C");
                uGraph.AddVertex("D");
                uGraph.AddVertex("E");
                uGraph.AddVertex("F");
                uGraph.AddVertex("G");
                uGraph.AddVertex("H");

                uGraph.AddEdge("A", "F", 10);
                uGraph.AddEdge("A", "B", 28);
                uGraph.AddEdge("B", "H", 13);
                uGraph.AddEdge("F", "E", 25);
                uGraph.AddEdge("B", "G", 14);
                uGraph.AddEdge("B", "C", 16);
                uGraph.AddEdge("E", "G", 24);
                uGraph.AddEdge("E", "D", 22);
                uGraph.AddEdge("G", "D", 18);
                uGraph.AddEdge("D", "C", 12);
                UGraphAL<string> minSpanningTree = (UGraphAL<string>)uGraph.MinimumSpanningTree();
                Console.WriteLine("======== Test Minimum Spanning Tree for ArrayList Graph ============");
                Console.WriteLine(minSpanningTree.ToString());
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Exception testing MST " + ex);
            }
        }
        static void TestShortestPathAL()
        {
            UGraphAL<string> uGraph = new UGraphAL<string>();
            try
            {
                uGraph.AddVertex("A");
                uGraph.AddVertex("B");
                uGraph.AddVertex("C");
                uGraph.AddVertex("D");
                uGraph.AddVertex("E");
                uGraph.AddVertex("F");
                uGraph.AddVertex("G");
                uGraph.AddVertex("H");

                uGraph.AddEdge("A", "F", 10);
                uGraph.AddEdge("A", "B", 28);
                uGraph.AddEdge("B", "H", 13);
                uGraph.AddEdge("F", "E", 25);
                uGraph.AddEdge("B", "G", 14);
                uGraph.AddEdge("B", "C", 16);
                uGraph.AddEdge("E", "G", 24);
                uGraph.AddEdge("E", "D", 22);
                uGraph.AddEdge("G", "D", 18);
                uGraph.AddEdge("D", "C", 12);
                UGraphAL<string> shortestPath = (UGraphAL<string>)uGraph.ShortestWeightedPath("A", "D");
                Console.WriteLine("======== Test ShortestPath for ArrayList Graph ============");
                Console.WriteLine(shortestPath.ToString());
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Exception testing ShortestPath " + ex);
            }
        }
        static void TestShortestPath()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            try
            {
                uGraph.AddVertex("A");
                uGraph.AddVertex("B");
                uGraph.AddVertex("C");
                uGraph.AddVertex("D");
                uGraph.AddVertex("E");
                uGraph.AddVertex("F");
                uGraph.AddVertex("G");
                uGraph.AddVertex("H");

                uGraph.AddEdge("A", "F", 10);
                uGraph.AddEdge("A", "B", 28);
                uGraph.AddEdge("B", "H", 13);
                uGraph.AddEdge("F", "E", 25);
                uGraph.AddEdge("B", "G", 14);
                uGraph.AddEdge("B", "C", 16);
                uGraph.AddEdge("E", "G", 24);
                uGraph.AddEdge("E", "D", 22);
                uGraph.AddEdge("G", "D", 18);
                uGraph.AddEdge("D", "C", 12);
                UGraphMatrix<string> shortestPath = (UGraphMatrix<string>)uGraph.ShortestWeightedPath("A", "D");
                Console.WriteLine("======== Test ShortestPath for Matrix Graph ============");
                Console.WriteLine(shortestPath.ToString());
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Exception testing ShortestPath " + ex);
            }
        }
        static void TestRemoveVertex()
        {
            UGraphAL<string> uGraph = new UGraphAL<string>();
            try
            {
                uGraph.AddVertex("A");
                uGraph.AddVertex("B");
                uGraph.AddVertex("C");
                uGraph.AddVertex("D");
                uGraph.AddVertex("E");
                uGraph.AddVertex("F");
                uGraph.AddVertex("G");
                uGraph.AddVertex("H");

                uGraph.AddEdge("A", "F", 10);
                uGraph.AddEdge("A", "B", 28);
                uGraph.AddEdge("B", "H", 13);
                uGraph.AddEdge("F", "E", 25);
                uGraph.AddEdge("B", "G", 14);
                uGraph.AddEdge("B", "C", 16);
                uGraph.AddEdge("E", "G", 24);
                uGraph.AddEdge("E", "D", 22);
                uGraph.AddEdge("G", "D", 18);
                uGraph.AddEdge("D", "C", 12);
                Console.WriteLine("========Graph before remove Vertex E============");
                Console.WriteLine(uGraph.ToString());
                Console.WriteLine("========Graph after remove Vertex E============");
                uGraph.RemoveVertex("A");
                Console.WriteLine(uGraph.ToString());
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Exception testing RemoveEdge " + ex);
            }
        }
        static void TestRemoveEdge()
        {
            UGraphAL<string> uGraph = new UGraphAL<string>();
            try
            {
                uGraph.AddVertex("A");
                uGraph.AddVertex("B");
                uGraph.AddVertex("C");
                uGraph.AddVertex("D");
                uGraph.AddVertex("E");
                uGraph.AddVertex("F");
                uGraph.AddVertex("G");
                uGraph.AddVertex("H");

                uGraph.AddEdge("A", "F", 10);
                uGraph.AddEdge("A", "B", 28);
                uGraph.AddEdge("B", "H", 13);
                uGraph.AddEdge("F", "E", 25);
                uGraph.AddEdge("B", "G", 14);
                uGraph.AddEdge("B", "C", 16);
                uGraph.AddEdge("E", "G", 24);
                uGraph.AddEdge("E", "D", 22);
                uGraph.AddEdge("G", "D", 18);
                uGraph.AddEdge("D", "C", 12);
                Console.WriteLine("========Graph before remove Edge B - H ============");
                Console.WriteLine(uGraph.ToString());
                Console.WriteLine("========Graph after remove Edge B - H ============");
                uGraph.RemoveEdge("B", "H");
                Console.WriteLine(uGraph.ToString());
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Exception testing RemoveEdge " + ex);
            }
        }
        static void Main(string[] args)
        {
            /*IGraph<string> uGraph = new UGraphMatrix<string>();
            AddData(uGraph);
            Console.WriteLine(uGraph.ToString());
            Console.WriteLine("=====BreadFirstTraveral======");
            TestBreadFirstTraversal("REG", uGraph);*/

            //TestMST();


            /*IGraph<string> uGraphAL = new UGraphAL<string>();
            AddData(uGraphAL);
            Console.WriteLine(uGraphAL.ToString());
            Console.WriteLine("=====BreadFirstTraveral======");
            TestBreadFirstTraversal("REG", uGraphAL);*/

            TestMSTAL();
            TestShortestPath();
            TestShortestPathAL();
            TestRemoveVertex();
            TestRemoveEdge();
        }
    }
}
