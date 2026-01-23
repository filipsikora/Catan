using Catan.Application.Snapshots;

namespace Catan.Application.Queries.Board
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