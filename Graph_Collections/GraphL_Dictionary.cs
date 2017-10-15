using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Collections
{
    public class GraphL_Dictionary : IGraph
    {
        public class Vertex : IEquatable<Vertex>
        {
            public string Name;
            public Vertex(string name)
            {
                this.Name = name;
            }

            public bool Equals(Vertex other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Name, other.Name);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Vertex) obj);
            }

            public override int GetHashCode()
            {
                int hash = (Name != null ? Name.GetHashCode() : 0);
                return hash;
            }
        }
        public class Edge
        {
            public Vertex from;
            public Vertex to;
            public int distance;
            public Edge(Vertex from, Vertex to, int distance)
            {
                this.from = from;
                this.to = to;
                this.distance = distance;
            }
        }

        private Dictionary<Vertex, Dictionary<Vertex, Edge>> graph = new Dictionary<Vertex, Dictionary<Vertex, Edge>>();

        public void AddVertex(string str)
        {
            if (graph.ContainsKey(new Vertex(str)))
                return;
            graph[new Vertex(str)] = new Dictionary<Vertex, Edge>();
        }

        public void AddEdge(string str1, string str2, int num)
        {
            Vertex v1 = new Vertex(str1);
            Vertex v2 = new Vertex(str2);
            if(graph[v1].ContainsKey(v2) && graph[v2].ContainsKey(v1))
                return;
            graph[v1].Add(v2, new Edge(v1, v2, num));
            graph[v2].Add(v1, new Edge(v2, v1, num));
        }

        public int DelEdge(string str1, string str2)
        {
            Vertex v1 = new Vertex(str1);
            Vertex v2 = new Vertex(str2);
            if (graph.ContainsKey(v1) && graph.ContainsKey(v2))
            {
                if (graph[v1].ContainsKey(v2) && graph[v2].ContainsKey(v1))
                {
                    int distance = graph[v1][v2].distance;
                    graph[v1].Remove(v2);
                    graph[v2].Remove(v1);
                    return distance;
                }
                throw new EdgeNotFoundException();
            }
            throw new VertexNotFoundException();
        }

        public void DelVertex(string str)
        {
            Vertex v = new Vertex(str);
            if(!graph.ContainsKey(v))
                throw new VertexNotFoundException();
            foreach (var vertex in graph.Keys)
            {
                if (graph[vertex].ContainsKey(v))
                {
                    graph[vertex].Remove(v);
                }
            }
            graph.Remove(v);
        }

        public void Print()
        {
            foreach (var pair in graph)
            {
                Console.WriteLine("From {0}:", pair.Key.Name);
                foreach (var item in pair.Value)
                {
                    Console.WriteLine("\t to {0}: {1}", item.Key.Name, item.Value.distance);
                }
            }
        }

        public int GetEdge(string str1, string str2)
        {
            Vertex v1 = new Vertex(str1);
            Vertex v2 = new Vertex(str2);
            return graph[v1][v2].distance;
        }

        public void SetEdge(string str1, string str2, int num)
        {
            Vertex v1 = new Vertex(str1);
            Vertex v2 = new Vertex(str2);
            if (graph.ContainsKey(v1) && graph.ContainsKey(v2))
            {
                if (graph[v1].ContainsKey(v2) && graph[v2].ContainsKey(v1))
                {
                    graph[v1][v2] = new Edge(v1, v2, num);
                    graph[v2][v1] = new Edge(v2, v1, num);
                }
                else
                    throw new EdgeNotFoundException();
            }
            else
                throw new VertexNotFoundException();
        }

        public int VerticesCount()
        {
            return graph.Count;
        }

        public int EdgesCount()
        {
            int count = 0;

            foreach (var dictionary in graph.Values)
            {
                foreach (var edge in dictionary.Values)
                {
                    count++;
                }
            }
            return count / 2;
        }

        public int GetInputEdgeCount(string city)
        {
            throw new NotImplementedException();
        }

        public int GetOutputEdgeCount(string city)
        {
            throw new NotImplementedException();
        }

        public List<string> GetInputVertexNames(string city)
        {
            throw new NotImplementedException();
        }

        public List<string> GetOutputVertexNames(string city)
        {
            throw new NotImplementedException();
        }

        public int?[] Dijkstra(string city)
        {
            throw new NotImplementedException();
        }
    }
    
}
