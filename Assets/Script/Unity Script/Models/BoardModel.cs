using Catan.Shared.Dtos.UiMessages;
using System.Collections.Generic;
using Unity.Catan.Models;

namespace Catan.Unity.Models
{
    public class BoardModel
    {
        public int BlockedHexId { get; set; }
        public int? SelectedVertexId { get; set; }
        public int? SelectedEdgeId { get; set; }
        public Dictionary<int, VertexModel> Vertices { get; set; }
        public Dictionary<int, EdgeModel> Edges { get; set; }
        public Dictionary<int, HexModel> Hexes { get; set; }
        public Dictionary<int, PortModel> Ports { get; set; }

        public BoardModel(int blockedHexId, int? selectedVertexId, int? selectedEdgeId, Dictionary<int, VertexModel> vertices, Dictionary<int, EdgeModel> edges, Dictionary<int, HexModel> hexes, Dictionary<int, PortModel> ports)
        {
            BlockedHexId = blockedHexId;
            SelectedVertexId = selectedVertexId;
            SelectedEdgeId = selectedEdgeId;
            Vertices = vertices;
            Edges = edges;
            Hexes = hexes;
            Ports = ports; 
        }
    }
}