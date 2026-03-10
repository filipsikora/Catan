#nullable enable
using Catan.Shared.Data;

namespace Catan.Core.Models
{
    public class Port
    {
        public Vertex VertexA;

        public Vertex VertexB;

        public Player Owner;

        public Edge Edge;

        public EnumResourceTypes? Type;

        public Port(Vertex a, Vertex b, Edge edge)
        {
            VertexA = a;
            VertexB = b;
            Edge = edge;
            Type = null;
        }

    }
}
