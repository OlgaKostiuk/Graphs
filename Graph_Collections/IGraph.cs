using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Collections
{
    public interface IGraph
    {
        void AddVertex(string str);                         //добавляет новый узел; даже если узел уже существует, исключения нет
        void AddEdge(string str1, string str2, int num);    //добавляет новое ребро; если указаных узлов нет, то они будут добавлены
        int DelEdge(string str1, string str2);              //возвращает вес удаленного ребра; исключение, если указанного ребра не было 
        void DelVertex(string str);                         //удаляет указанный узел и удаляет все ребра идущие от и к этому узлу; исключение, если указанного узла не было
        void Print();                                       //на консоль выводит все названия узлов по порядку в списке и ребра к другим узлам с их весом 
        int GetEdge(string str1, string str2);              //возвращает вес ребра; исключение, если такого ребра не было
        void SetEdge(string str1, string str2, int num);    //переустанавливает вес ребра; исключение, если нет указанных узлов или если есть узлы, но нет ребра

        int EdgesCount();
        int VerticesCount();

        int GetInputEdgeCount(string city);
        int GetOutputEdgeCount(string city);

        List<string> GetInputVertexNames(string city);
        List<string> GetOutputVertexNames(string city);

        int?[] Dijkstra(string city);
    }
}
