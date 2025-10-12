using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    internal class Edge
    {
        public Vertex VertexA { get; }

        public Vertex VertexB { get; }

        public Player? Owner { get; set; } = null;

        public float X { get; set; }

        public float Y { get; set; }


        public Edge(Vertex a, Vertex b)
        {
            VertexA = a;
            VertexB = b;

            X = (a.X + b.X) / 2;
            Y = (a.Y + b.Y) / 2;

            a.ConnectedEdges.Add(this);
            b.ConnectedEdges.Add(this);
        }


        public bool HasAccessToEdge(Player player)
        {
            return VertexA.HasAccessToVertex(player) || VertexB.HasAccessToVertex(player);
        }

        public Player? RoadOwner()
        {
            return Owner;
        }

        public bool IsOwned => Owner != null;

        public override string ToString()
        {
            return $"Edge owner {Owner}";
        }
    }
}
