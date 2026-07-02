namespace Catan.Unity.Models
{
    public class EdgeModel
    {
        public int EdgeId { get; set; }
        public int VertexAId { get; set; }
        public int VertexBId { get; set; }

        public int? OwnerId { get; set; }

        public EdgeModel(int edgeId, int vertexAId, int vertexBId, int? ownerId)
        {
            EdgeId = edgeId;
            VertexAId = vertexAId;
            VertexBId = vertexBId;
            OwnerId = ownerId;
        }
    }
}
