using System.Collections.Generic;
using Catan.Application.Snapshots;

namespace Catan.Application.Queries.DevCards
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