using Catan.Shared.Data;
using System.Collections.Generic;

namespace Unity.Catan.Models
{
    public class VertexModel
    {
        public int VertexId { get; set; }
        public List<(int HexQ, int HexR, int CornerIndex)> Corners { get; set; }

        public int? OwnerId { get; set; }
        public EnumBuildings Building { get; set; }

        public VertexModel(int vertexId, List<(int, int, int)> corners, int? ownerId, EnumBuildings building)
        {
            VertexId = vertexId;
            Corners = corners;
            OwnerId = ownerId;
            Building = building;
        }
    }
}
