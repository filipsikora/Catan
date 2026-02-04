using Catan.Core.Snapshots;

namespace Catan.Core.Queries.Interfaces
{
    public interface IBoardQueryService
    {
        EdgeSnapshot GetEdgeData(int edgeId);
        VertexSnapshot GetVertexData(int vertexId);
        HexSnapshot GetHexData(int hexId);
        PortSnapshot GetPortData(int edgeId);
        BoardSnapshot GetBoardData();
    }
}