using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots
{
    public sealed class EdgeSnapshot
    {
        public int EdgeId;
        public int VertexAId;
        public int VertexBId;

        public EdgeSnapshot(int edgeId, int vertexAId, int vertexBId)
        {
            EdgeId = edgeId;
            VertexAId = vertexAId;
            VertexBId = vertexBId;
        }
    }

    public sealed class VertexSnapshot
    {
        public int VertexId;
        public List<(int HexQ, int HexR, int CornerIndex)> Corners;

        public VertexSnapshot(int vertexId, List<(int HexQ, int HexR, int CornerIndex)> corners)
        {
            VertexId = vertexId;
            Corners = corners;
        }
    }

    public sealed class HexSnapshot
    {
        public int HexId;
        public int? HexNumber;
        public EnumFieldTypes? FieldType;
        public int Q;
        public int R;

        public HexSnapshot(int hexId, int? hexNumber, EnumFieldTypes? fieldType, int q, int r)
        {
            HexId = hexId;
            HexNumber = hexNumber;
            FieldType = fieldType;
            Q = q;
            R = r;
        }
    }

    public sealed class BoardSnapshot
    {
        public List<HexSnapshot> Hexes;
        public List<VertexSnapshot> Vertices;
        public List<EdgeSnapshot> Edges;
        public List<PortSnapshot> Ports;
        public int BlockedHexId;

        public BoardSnapshot(List<HexSnapshot> hexes, List<VertexSnapshot> vertices, List<EdgeSnapshot> edges, List<PortSnapshot> ports, int blockedHexId)
        {
            Hexes = hexes;
            Vertices = vertices;
            Edges = edges;
            Ports = ports;
            BlockedHexId = blockedHexId;
        }
    }

    public sealed class PortSnapshot
    {
        public int EdgeId;
        public EnumResourceTypes? Type;

        public PortSnapshot(int edgeId, EnumResourceTypes? type)
        {
            EdgeId = edgeId;
            Type = type;
        }
    }
}