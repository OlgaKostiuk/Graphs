using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //GraphL graph = new GraphL();
            //graph.AddVertex("A");
            //graph.AddVertex("B");
            //graph.AddVertex("C");
            //graph.AddVertex("D");

            //graph.AddEdge("A", "B", 1);
            //graph.AddEdge("A", "C", 55);
            //graph.AddEdge("B", "C", 2);
            //graph.AddEdge("C", "D", 3);
            //graph.AddEdge("D", "A", 4);

            //graph.Print();

            //Console.WriteLine("-------------------------");
            //graph.DelVertex("C");
            //graph.Print();

            //Console.WriteLine("-------------------------");
            //int a = graph.DelEdge("A", "B");
            //graph.Print();
            //Console.WriteLine("delEdge = {0}", a);

            //int c = graph.GetEdge("D", "A");
            //Console.WriteLine("edge = {0}", c);

            //graph.SetEdge("B", "C", 1001);
            //Console.WriteLine("-------------------------");
            //graph.Print();

            //Console.WriteLine("-------------------------");
            IGraph graph = new GraphM();
            string name = "A";
            for (int i = 0; i < 5; i++)
            {
                string temp = name;
                graph.AddVertex(temp);

                name = "A" + i;
                graph.AddVertex(name);

                graph.AddEdge(temp, name, 4);
            }
            graph.Print();

            graph.DelVertex("A2");
//            graph.Print();
            graph.AddEdge("A3", "C", 10);
            graph.Print();
            Console.WriteLine(graph.VerticesCount());

            GraphM testGreedy = new GraphM();
            testGreedy.AddVertex("1");
            testGreedy.AddVertex("2");
            testGreedy.AddVertex("3");
            testGreedy.AddVertex("4");

            testGreedy.AddEdge("1", "2", 10);
            testGreedy.AddEdge("2", "3", 5);
            testGreedy.AddEdge("1", "3", 20);
            testGreedy.AddEdge("2", "4", 12);
            testGreedy.AddEdge("3", "4", 1);
            testGreedy.Dijkstra("1");

            Console.ReadKey();
        }
    }
}
