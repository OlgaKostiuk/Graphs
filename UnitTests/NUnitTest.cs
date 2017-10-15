using System.Collections.Generic;
using Graph_Collections;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture(typeof(GraphL))]
    [TestFixture(typeof(GraphM))]
    public class Tests<TGraph> where TGraph : IGraph, new()
    {
        IGraph _graph;

        [SetUp]
        public void SetUp()
        {
            _graph = new TGraph();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestNumVerticesCount(int length)
        {
            for (int i = 0; i < length; i++)
            {
                _graph.AddVertex("A" + i);
            }
            Assert.AreEqual(length, _graph.VerticesCount());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestNumEdgesCount(int length)
        {
            string name = "A";
            for (int i = 0; i < length; i++)
            {
                string temp = name;
                _graph.AddVertex(temp);

                name = "A" + i;
                _graph.AddVertex(name);

                _graph.AddEdge(temp, name, 4);
            }
            Assert.AreEqual(length, _graph.EdgesCount());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddEdge(int length)
        {
            string name = "A";
            for (int i = 0; i < length; i++)
            {
                string temp = name;
                _graph.AddVertex(temp);

                name = "A" + i;
                _graph.AddVertex(name);

                _graph.AddEdge(temp, name, 4);
            }

            Assert.AreEqual(length, _graph.EdgesCount());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddExistingEdge(int length)
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");
            for (int i = 0; i < length; i++)
            {
                _graph.AddEdge("A", "B", 4);
            }
            Assert.AreEqual(1, _graph.EdgesCount());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestGetEdge(int length)
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");
            _graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, _graph.GetEdge("A", "B"));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestGetEdgeEx_NoVertex(string name)
        {
            _graph.AddVertex(name);

            var ex = Assert.Throws<VertexNotFoundException>(() => _graph.GetEdge("A", "B"));
            Assert.AreEqual(typeof(VertexNotFoundException), ex.GetType());
        }

        [Test]
        public void TestGetEdgeEx_NoEdgesCount()
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");

            var ex = Assert.Throws<EdgeNotFoundException>(() => _graph.GetEdge("A", "B"));
            Assert.AreEqual(typeof(EdgeNotFoundException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestDelEdge(int length)
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");
            _graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, _graph.DelEdge("A", "B"));
            Assert.AreEqual(0, _graph.EdgesCount());

        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestDelEdgeEx_NoVertex(string name)
        {
            _graph.AddVertex(name);

            var ex = Assert.Throws<VertexNotFoundException>(() => _graph.DelEdge("A", "B"));
            Assert.AreEqual(typeof(VertexNotFoundException), ex.GetType());
        }

        [Test]
        public void TestDelEdgeEx_NoEdge()
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");

            var ex = Assert.Throws<EdgeNotFoundException>(() => _graph.DelEdge("A", "B"));
            Assert.AreEqual(typeof(EdgeNotFoundException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestSetEdge(int length)
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");
            _graph.AddEdge("A", "B", 458);
            _graph.SetEdge("A", "B", length);
            Assert.AreEqual(length, _graph.GetEdge("A", "B"));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestSetEdgeEx_NoVertex(string name)
        {
            _graph.AddVertex(name);

            var ex = Assert.Throws<VertexNotFoundException>(() => _graph.SetEdge("A", "B", 2));
            Assert.AreEqual(typeof(VertexNotFoundException), ex.GetType());
        }

        [Test]
        public void TestSetEdgeEx_NoEdgesCountCount()
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");

            var ex = Assert.Throws<EdgeNotFoundException>(() => _graph.SetEdge("A", "B", 2));
            Assert.AreEqual(typeof(EdgeNotFoundException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestAddVertex(int length)
        {
            for (int i = 0; i < length; i++)
            {
                _graph.AddVertex("A" + i);
            }
            Assert.AreEqual(length, _graph.VerticesCount());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestAddSimilarVertex(int length)
        {
            for (int i = 0; i < length; i++)
            {
                _graph.AddVertex("A");
            }
            Assert.AreEqual(1, _graph.VerticesCount());
        }

        [TestCase("A")]
        [TestCase("Bdfd")]
        [TestCase("")]
        public void TestDelVertex(string name)
        {
            _graph.AddVertex(name);
            _graph.DelVertex(name);
            Assert.AreEqual(0, _graph.VerticesCount());
        }

        [Test]
        public void TestDelVertexEx_NoVertex()
        {
            _graph.AddVertex("Test");

            var ex = Assert.Throws<VertexNotFoundException>(() => _graph.DelVertex("DelTest"));
            Assert.AreEqual(typeof(VertexNotFoundException), ex.GetType());
        }

        [Test]
        public void TestDelVertex_EdgesCountCount()
        {
            _graph.AddVertex("A");
            _graph.AddVertex("B");
            _graph.AddVertex("C");

            _graph.AddEdge("A", "B", 1);
            _graph.AddEdge("B", "C", 2);
            _graph.AddEdge("C", "A", 3);

            _graph.DelVertex("B");

            Assert.AreEqual(1, _graph.EdgesCount());
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestGetInputEdgeCount(int n)
        {
            _graph.AddVertex("A");
            for (int i = 0; i < 5; i++)
            {
                _graph.AddVertex(i.ToString());
            }

            for (int i = 0; i < n; i++)
            {
                _graph.AddEdge(i.ToString(), "A", i);
            }

            Assert.AreEqual(6, _graph.VerticesCount());
            Assert.AreEqual(n, _graph.GetInputEdgeCount("A"));
        }

        [Test]
        public void TestGetInputEdgeCountEx()
        {
            Assert.Throws<VertexNotFoundException>(() => _graph.GetInputEdgeCount("A"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestGetOutputEdgeCount(int n)
        {
            _graph.AddVertex("A");
            for (int i = 0; i < 5; i++)
            {
                _graph.AddVertex(i.ToString());
            }

            for (int i = 0; i < n; i++)
            {
                _graph.AddEdge("A", i.ToString(), i);
            }

            Assert.AreEqual(6, _graph.VerticesCount());
            Assert.AreEqual(n, _graph.GetOutputEdgeCount("A"));
        }

        [Test]
        public void TestGetOutputEdgeCountEx()
        {
            Assert.Throws<VertexNotFoundException>(() => _graph.GetOutputEdgeCount("A"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestGetInputVertexNames(int n)
        {
            _graph.AddVertex("A");
            for (int i = 0; i < 5; i++)
            {
                _graph.AddVertex(i.ToString());
            }

            for (int i = 0; i < n; i++)
            {
                _graph.AddEdge(i.ToString(), "A", i);
            }

            string[] expected = new string[n];
            for (int i = 0; i < n; i++)
            {
                expected[i] = i.ToString();
            }

            Assert.AreEqual(6, _graph.VerticesCount());
            CollectionAssert.AreEqual(expected, _graph.GetInputVertexNames("A"));
        }

        [Test]
        public void TestGetInputVertexNamesEx()
        {
            Assert.Throws<VertexNotFoundException>(() => _graph.GetInputVertexNames("A"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestGetOutputVertexNames(int n)
        {
            _graph.AddVertex("A");
            for (int i = 0; i < 5; i++)
            {
                _graph.AddVertex(i.ToString());
            }

            for (int i = 0; i < n; i++)
            {
                _graph.AddEdge("A", i.ToString(), i);
            }

            string[] expected = new string[n];
            for (int i = 0; i < n; i++)
            {
                expected[i] = i.ToString();
            }

            Assert.AreEqual(6, _graph.VerticesCount());
            CollectionAssert.AreEqual(expected, _graph.GetOutputVertexNames("A"));
        }

        [Test]
        public void TestGetOutputVertexNamesEx()
        {
            Assert.Throws<VertexNotFoundException>(() => _graph.GetOutputVertexNames("A"));
        }
    }
}