using System.Collections.Generic;
using FluentAssertions;
using Graph_Collections;
using NUnit.Framework;

namespace UnitTests
{

    [TestFixture(typeof(GraphM))]
    public class UnitTest1<TGraph> where TGraph : IGraph, new()
    {
        IGraph _graph;

        [SetUp]
        public void SetUp()
        {
            _graph = new TGraph();
        }

        [Test]
        public void TestDijkstra()
        {
            _graph.AddEdge("A", "B", 10);
            _graph.AddEdge("B", "C", 5);
            _graph.AddEdge("A", "C", 20);
            _graph.AddEdge("B", "D", 12);
            _graph.AddEdge("C", "D", 1);
            var result = _graph.Dijkstra("A");
            //result.ShouldBeEquivalentTo(new int?[]{1,2,3});
            result[0].Should().Be(0);
            result[1].Should().Be(10);
            result[2].Should().Be(15);
            result[3].Should().Be(16);
        }

        [Test]
        public void TestDijkstraAfterDelEdge()
        {
            _graph.AddEdge("A", "B", 10);
            _graph.AddEdge("B", "C", 5);
            _graph.AddEdge("A", "C", 20);
            _graph.AddEdge("B", "D", 12);
            _graph.AddEdge("C", "D", 1);
            _graph.DelEdge("B", "C");
            var result = _graph.Dijkstra("A");
            result[0].Should().Be(0);
            result[1].Should().Be(10);
            result[2].Should().Be(20);
            result[3].Should().Be(21);
        }

        [Test]
        public void TestDijkstraAfterDelVertex()
        {
            _graph.AddEdge("A", "B", 10);
            _graph.AddEdge("B", "C", 5);
            _graph.AddEdge("A", "C", 20);
            _graph.AddEdge("B", "D", 12);
            _graph.AddEdge("C", "D", 1);
            _graph.DelVertex("C");
            var result = _graph.Dijkstra("A");
            result[0].Should().Be(0);
            result[1].Should().Be(10);
            result[2].Should().BeNull();
            result[3].Should().Be(22);
        }

        [Test]
        public void TestDijkstraAfterDelVertex2()
        {
            _graph.AddEdge("A", "B", 10);
            _graph.AddEdge("B", "C", 5);
            _graph.AddEdge("A", "C", 20);
            _graph.AddEdge("B", "D", 12);
            _graph.AddEdge("C", "D", 1);
            var result = _graph.Dijkstra("C");
            result[0].Should().Be(int.MaxValue);
            result[1].Should().Be(int.MaxValue);
            result[2].Should().Be(0);
            result[3].Should().Be(1);
        }
    }
}