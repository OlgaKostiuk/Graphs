using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Collections
{
    public class GraphL : IGraph
    {
        public class Vertex
        {
            public string Name;
            public List<Edge> Edges = new List<Edge>();
            public Vertex(string str)
            {
                Name = str;
            }
        }

        public class Edge
        {
            public Vertex From;
            public Vertex To;
            public int Weight;

            public Edge(Vertex from, Vertex to, int weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }
        }

        private List<Vertex> _vertices = new List<Vertex>();

        private Vertex GetVertexRef(string str)
        {
            return _vertices.FirstOrDefault((x) => x.Name == str);
        }

        private Edge GetEdgeRef(string str1, string str2)
        {
            Edge res = null;
            Vertex from = GetVertexRef(str1);
            Vertex to = GetVertexRef(str2);
            if (from != null && to != null)
            {
                foreach (var edge in from.Edges)
                {
                    if (edge.To == to)
                    {
                        res = edge;
                        break;
                    }
                }
            }
            return res;
        }

        public void AddEdge(string str1, string str2, int num)
        {
            Vertex from = GetVertexRef(str1);
            Vertex to = GetVertexRef(str2);
            if (from == null)
            {
                AddVertex(str1);
                from = GetVertexRef(str1);
            }
            if (to == null)
            {
                AddVertex(str2);
                to = GetVertexRef(str2);
            }
            if (GetEdgeRef(str1, str2) == null)
            {
                Edge edge = new Edge(from, to, num);
                from.Edges.Add(edge);
            }
        }

        public void AddVertex(string str)
        {
            if (GetVertexRef(str) == null)
                _vertices.Add(new Vertex(str));
        }

        public int DelEdge(string str1, string str2)
        {
            if(GetVertexRef(str1) == null || GetVertexRef(str2) == null)
                throw new VertexNotFoundException();
            Edge edge = GetEdgeRef(str1, str2);
            if(edge == null)
                throw new EdgeNotFoundException();
            int res = edge.Weight;
            edge.From.Edges.Remove(edge);
            return res;
        }

        public void DelVertex(string str)
        {
            Vertex vertex = GetVertexRef(str);
            if(vertex == null)
                throw new VertexNotFoundException();
            foreach (var v in _vertices)
            {
                v.Edges.RemoveAll(x => x.To == vertex);
            }
            _vertices.Remove(vertex);
        }

        public int GetEdge(string str1, string str2)
        {
            if(GetVertexRef(str1) == null || GetVertexRef(str2) == null)
                throw new VertexNotFoundException();
            Edge edge = GetEdgeRef(str1, str2);
            if(edge == null)
                throw new EdgeNotFoundException();
            return edge.Weight;
        }

        public void Print()
        {
            Console.WriteLine("Vertices: {0}, Edges: {1}", VerticesCount(), EdgesCount());
            foreach (Vertex vertex in _vertices)
            {
                Console.WriteLine("From {0}:", vertex.Name);
                foreach (Edge edge in vertex.Edges)
                {
                    Console.WriteLine("\t to {0}: {1}", edge.To.Name, edge.Weight);
                }
            }
        }

        public void SetEdge(string str1, string str2, int num)
        {
            if(GetVertexRef(str1) == null || GetVertexRef(str2)== null)
                throw new VertexNotFoundException();
            Edge edge = GetEdgeRef(str1, str2);
            if(edge == null)
                throw new EdgeNotFoundException();
            edge.Weight = num;
        }

        public int EdgesCount()
        {
                int count = 0;
                foreach (var vertex in _vertices)
                {
                    foreach (var edge in vertex.Edges)
                    {
                        count++;
                    }
                }
                return count;
        }

        public int VerticesCount()
        {
            return _vertices.Count;
        }

        public int GetInputEdgeCount(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if(vertex == null)
                throw new VertexNotFoundException();
            int count = 0;
            foreach (var v in _vertices)
            {
                count += v.Edges.FindAll(x => x.To == vertex).Count;
            }
            return count;
        }

        public int GetOutputEdgeCount(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if(vertex == null)
                throw new VertexNotFoundException();
            return vertex.Edges.Count;
        }

        public List<string> GetInputVertexNames(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if (vertex == null)
                throw new VertexNotFoundException();
            List<string> res = new List<string>();
            foreach (var v in _vertices)
            {
                res.AddRange(v.Edges.FindAll(x=>x.To==vertex).Select(x=>x.From.Name));
            }
            return res;
        }

        public List<string> GetOutputVertexNames(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if (vertex == null)
                throw new VertexNotFoundException();
            List<string> res = new List<string>();
            foreach (var edge in vertex.Edges)
            {
                res.Add(edge.To.Name);
            }
            return res;
        }

        public int?[] Dijkstra(string city)
        {
            throw new NotImplementedException();
        }
    }
}
