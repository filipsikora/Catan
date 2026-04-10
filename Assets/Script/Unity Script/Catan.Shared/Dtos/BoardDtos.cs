using System.Collections.Generic;

namespace Catan.Shared.Dtos
{
    public class CornerDto
    {
        public int HexQ { get; set; }
        public int HexR { get; set; }
        public int CornerIndex { get; set; }
    }

    public class VertexDto
    {
        public int VertexId { get; set; }
        public List<CornerDto> Corners { get; set; }
    }

    public class EdgeDto
    {
        public int EdgeId { get; set; }
        public int VertexAId { get; set; }
        public int VertexBId { get; set; }
    }

    public class HexDto
    {
        public int HexId { get; set; }
        public int? HexNumber { get; set; }
        public string? FieldType { get; set; }
        public int Q { get; set; }
        public int R { get; set; }
    }

    public class PortDto
    {
        public int EdgeId { get; set; }
        public string? Type { get; set; }
    }

    public class BoardDto
    {
        public List<VertexDto> Vertices { get; set; }
        public List<EdgeDto> Edges { get; set; }
        public List<HexDto> Hexes { get; set; }
        public List<PortDto> Ports { get; set; }
        public int BlockedHexId { get; set; }
    }
}
