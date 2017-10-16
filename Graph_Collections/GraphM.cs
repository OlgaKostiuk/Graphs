using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Collections
{
    public class GraphM : IGraph
    {
        private class Vertex
        {
            public string Name;
            public Vertex prev;

            public Vertex(string name)
            {
                Name = name;
            }
        }
        private class Edge
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

        private static readonly int _size = 24;
        private Vertex[] _vertices = new Vertex[_size];
        private Edge[,] _matrix = new Edge[_size, _size];


        public void AddVertex(string str)
        {
            if (GetVertexRef(str) != null)
                return;
            int index = Array.IndexOf(_vertices, null);
            _vertices[index] = new Vertex(str);
        }

        public void AddEdge(string str1, string str2, int num)
        {
            Vertex vertex1 = GetVertexRef(str1);
            Vertex vertex2 = GetVertexRef(str2);

            if (vertex1 == null)
            {
                AddVertex(str1);
                vertex1 = GetVertexRef(str1);
            }
            if (vertex2 == null)
            {
                AddVertex(str2);
                vertex2 = GetVertexRef(str2);
            }
            int index1 = Array.IndexOf(_vertices, vertex1);
            int index2 = Array.IndexOf(_vertices, vertex2);
            _matrix[index1, index2] = new Edge(vertex1, vertex2, num);
        }

        public int DelEdge(string str1, string str2)
        {
            Vertex v1 = GetVertexRef(str1);
            Vertex v2 = GetVertexRef(str2);

            if (v1 == null || v2 == null)
                throw new VertexNotFoundException();

            int index1 = Array.IndexOf(_vertices, v1);
            int index2 = Array.IndexOf(_vertices, v2);

            if (_matrix[index1, index2] == null)
                throw new EdgeNotFoundException();

            int weight = _matrix[index1, index2].Weight;
            _matrix[index1, index2] = null;
            return weight;
        }

        public void DelVertex(string str)
        {
            Vertex vertex = GetVertexRef(str);
            if (vertex == null)
                throw new VertexNotFoundException();

            int index = Array.IndexOf(_vertices, vertex);

            for (int i = 0; i < _size; i++)
            {
                _matrix[index, i] = null;
                _matrix[i, index] = null;
            }
            _vertices[index] = null;
        }

        public void Print()
        {
            Console.Write("___");
            for (int i = 0; i < _size; i++)
            {
                Console.Write("{0, 2}|", i);
            }
            Console.WriteLine();
            for (int i = 0; i < _size; i++)
            {
                Console.Write("{0, 2}|", i);
                for (int j = 0; j < _size; j++)
                {
                    if (_matrix[i, j] == null)
                        Console.Write("{0, 2}|", "");
                    else
                        Console.Write("{0, 2}|", _matrix[i, j].Weight);
                }
                Console.WriteLine();
            }
        }

        public int GetEdge(string str1, string str2)
        {
            Vertex v1 = GetVertexRef(str1);
            Vertex v2 = GetVertexRef(str2);

            if (v1 == null || v2 == null)
                throw new VertexNotFoundException();
            Edge edge = GetEdgeRef(str1, str2);
            if (edge == null)
                throw new EdgeNotFoundException();
            return edge.Weight;
        }

        public void SetEdge(string str1, string str2, int num)
        {
            Vertex v1 = GetVertexRef(str1);
            Vertex v2 = GetVertexRef(str2);

            if (v1 == null || v2 == null)
                throw new VertexNotFoundException();

            Edge edge = GetEdgeRef(str1, str2);
            if (edge == null)
                throw new EdgeNotFoundException();

            edge.Weight = num;
        }

        public int EdgesCount()
        {
            int count = 0;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (_matrix[i, j] != null)
                        count++;
                }
            }
            return count;
        }

        public int VerticesCount()
        {
            int count = 0;
            foreach (var v in _vertices)
            {
                if (v != null)
                    count++;
            }
            return count;
        }

        public int GetInputEdgeCount(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if (vertex == null)
                throw new VertexNotFoundException();
            int count = 0;
            int index = Array.IndexOf(_vertices, vertex);
            for (int i = 0; i < _size; i++)
            {
                if (_matrix[i, index] != null)
                    count++;
            }
            return count;
        }

        public int GetOutputEdgeCount(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if (vertex == null)
                throw new VertexNotFoundException();
            int count = 0;
            int index = Array.IndexOf(_vertices, vertex);
            for (int i = 0; i < _size; i++)
            {
                if (_matrix[index, i] != null)
                    count++;
            }
            return count;
        }

        public List<string> GetInputVertexNames(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if (vertex == null)
                throw new VertexNotFoundException();
            List<string> names = new List<string>();
            int index = Array.IndexOf(_vertices, vertex);
            for (int i = 0; i < _size; i++)
            {
                Edge edge = _matrix[i, index];
                if (edge != null)
                    names.Add(edge.From.Name);
            }
            return names;
        }

        public List<string> GetOutputVertexNames(string city)
        {
            Vertex vertex = GetVertexRef(city);
            if (vertex == null)
                throw new VertexNotFoundException();
            List<string> names = new List<string>();
            int index = Array.IndexOf(_vertices, vertex);
            for (int i = 0; i < _size; i++)
            {
                Edge edge = _matrix[index, i];
                if (edge != null)
                    names.Add(edge.To.Name);
            }
            return names;
        }

        private Edge GetEdgeRef(string str1, string str2)
        {
            Vertex vertex1 = GetVertexRef(str1);
            Vertex vertex2 = GetVertexRef(str2);

            if (vertex1 == null || vertex2 == null)
                return null;
            int index1 = Array.IndexOf(_vertices, vertex1);
            int index2 = Array.IndexOf(_vertices, vertex2);
            return _matrix[index1, index2];
        }

        private Vertex GetVertexRef(string str)
        {
            return _vertices.FirstOrDefault(x => x != null && x.Name == str);
        }


        public int?[] Dijkstra(string city)
        {
            Vertex src = GetVertexRef(city);
            int?[] dist = new int?[_size];
            bool?[] sptSet = new bool?[_size];
            //Vertex[] parents = new Vertex[_size];
            for (int i = 0; i < _size; i++)
            {
                if (_vertices[i] != null)
                {
                    dist[i] = int.MaxValue;
                    sptSet[i] = false;
                }
            }
            int index = Array.IndexOf(_vertices, src);
            dist[index] = 0;

            for (int count = 0; count < _size - 1; count++)
            {
                int current = minDistance(dist, sptSet);
                if(current == -1)
                    break;
                sptSet[current] = true;
                for (int v = 0; v < _size; v++)
                {
                    if (sptSet[v] != null && sptSet[v] == false &&
                        _matrix[current, v] != null &&
                        dist[current] != int.MaxValue &&
                        dist[current] + _matrix[current, v].Weight < dist[v])
                    {
                        dist[v] = dist[current] + _matrix[current, v].Weight;
                        _vertices[v].prev = _vertices[current];
                        //parents[v] = _vertices[current];
                    }
                }
            }
            printSolution(dist);
            return dist;
        }

        private int minDistance(int?[] dist, bool?[] sptSet)
        {
            int min = int.MaxValue;
            int minIndex = -1;

            for (int v = 0; v < _size; v++)
            {
                if (sptSet[v] != null && sptSet[v] == false && dist[v].Value <= min)
                {
                    min = dist[v].Value;
                    minIndex = v;
                }
            }
            return minIndex;
        }

        void printSolution(int?[] dist)
        {
            for (int i = 0; i < _size; i++)
                if (dist[i] != null)
                {
                    List<Vertex> path = new List<Vertex>();
                    Console.Write("Vertex: " + _vertices[i].Name + " Distance: " + dist[i]  + " Path: ");
                    Vertex previous = _vertices[i].prev;
                    while (previous != null)
                    {
                        path.Add(previous);
                        previous = previous.prev;
                    }
                    path.Reverse();
                    foreach (var item in path)
                    {
                        Console.Write(item.Name + "->");
                    }
                    Console.Write(_vertices[i].Name + "\n");
                }
        }
    }
}

