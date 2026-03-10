#nullable enable
using System.Collections.Generic;
using Catan.Core.Interfaces;

namespace Catan.Core.Models
{
    public class Edge : IPositionData
    {
        public Vertex VertexA { get; }

        public Vertex VertexB { get; }

        public Player? Owner { get; set; } = null;

        public float X { get; set; }

        public float Y { get; set; }

        public int Id { get; set; }

        public List<Edge> ConnectedEdges = new();

        public bool IsNextToVertex(Vertex vertex)
        {
            return VertexA == vertex || VertexB == vertex;
        }

        public Edge(Vertex a, Vertex b)
        {
            VertexA = a;
            VertexB = b;

            X = (a.X + b.X) / 2;
            Y = (a.Y + b.Y) / 2;

            a.ConnectedEdges.Add(this);
            b.ConnectedEdges.Add(this);
        }


        public bool AccessibleByPlayer(Player player)
        {
            return VertexA.AccessibleByPlayer(player) || VertexB.AccessibleByPlayer(player);
        }

        public Player? RoadOwner()
        {
            return Owner;
        }

        public bool IsOwned => Owner != null;

        public override string ToString()
        {
            return $"Edge_{Id}";
        }
    }
}
