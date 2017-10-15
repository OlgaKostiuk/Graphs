using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Collections
{
    public class VertexNotFoundException : Exception
    {
        public override string Message => "Vertex not found";
    }
}
