using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    internal class Program
    {
        static void SorterTest()
        {
            Console.WriteLine("Enter number of elements: ");
            string input = Console.ReadLine();
            int arraySize = Int32.Parse(input);
            int[] array = new int[arraySize];
            Random r = new Random(Environment.TickCount);
            for(int i = 0; i < arraySize; i++)
            {
                array[i] = r.Next(array.Length);
            }
            //TestSorter(new InsertionSorter<int>(array));
            //TestSorter(new HeapSorter<int>(array));
            TestSorter(new QuickSorter<int>(array));
        }
        static void TestSorter(ASorter<int> sorter)
        {
            Console.WriteLine(sorter.GetType().Name + " with " + sorter.Length + " elements.");
            if(sorter.Length < 2000000)
            {
                Console.WriteLine("Before sort: \n" + sorter);
            }
            long startTime = Environment.TickCount;
            sorter.Sort();
            long endTime = Environment.TickCount;
            if(sorter.Length < 20000000)
            {
                Console.WriteLine("After sort: \n" + sorter);
            }
            Console.WriteLine("Sort took " + (endTime - startTime) + " miliseconds.");
        }
        static void Main(string[] args)
        {
            SorterTest();
        }
    }
}
