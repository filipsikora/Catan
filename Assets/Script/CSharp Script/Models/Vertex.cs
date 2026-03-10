#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Catan.Core.Interfaces;

namespace Catan.Core.Models
{
    public class Vertex : IPositionData
    {
        public int KeyX { get; }

        public int KeyY { get; }

        public float X { get; }

        public float Y { get; }

        public List<HexTile> AdjacentHexTiles { get; } = new List<HexTile>();

        public bool HasVillage { get; set; } = false;

        public bool HasTown { get; set; } = false;

        public bool HasPort { get; set; } = false;

        public List<Edge> ConnectedEdges { get; } = new List<Edge>();

        public List<Vertex> NeighbourVertices => ConnectedEdges.Select(e => e.VertexA == this ? e.VertexB : e.VertexA).ToList();

        public Player? Owner { get; set; } = null;

        public bool IsOwned => Owner != null;

        public int Id { get; set; }

        public Port? Port { get; set; } = null;

        public Vertex(float x, float y, int quant = 1000)
        {
            X = x; Y = y;
            KeyX = (int)Math.Round(x * quant);
            KeyY = (int)Math.Round(y * quant);
        }

        public bool AccessibleByPlayer(Player player)
        {
            return (Owner == player) || (ConnectedEdges.Any(e => e.Owner == player));
        }

        public override string ToString()
        {
            return $"Vertex_{Id}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Vertex v && v.KeyX == KeyX && v.KeyY == KeyY;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(KeyX, KeyY);
        }
    }
}
