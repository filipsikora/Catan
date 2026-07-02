using Catan.Shared.Dtos;
using Catan.Unity.Models;
using System.Collections.Generic;
using System.Linq;
using Unity.Catan.Models;

namespace Catan.Unity.Mappers
{
    public static class BoardMappers
    {
        public static BoardModel MapBoardStateToModel(GameStatePerPlayerDto gameState)
        {
            var board = gameState.Board;

            return new BoardModel(board.BlockedHexId, null, null, MapVerticesDtoToDictionary(board.Vertices), MapEdgesDtoToDictionary(board.Edges), MapHexesDtoToDictionary(board.Hexes), 
                MapPortsDtoToDictionary(board.Ports));
        }

        public static Dictionary<int, VertexModel> MapVerticesDtoToDictionary(List<FullVertexDto> verticesList)
        {
            return verticesList.ToDictionary(
                vertex => vertex.VertexId,
                vertex => new VertexModel(vertex.VertexId, MapCornersDtoToList(vertex.Corners), vertex.OwnerId, vertex.Building)
                );
        }

        public static Dictionary<int, EdgeModel> MapEdgesDtoToDictionary(List<FullEdgeDto> edgesList)
        {
            return edgesList.ToDictionary(
                edge => edge.EdgeId,
                edge => new EdgeModel(edge.EdgeId, edge.VertexAId, edge.VertexBId, edge.OwnerId)
                );
        }

        public static Dictionary<int, HexModel> MapHexesDtoToDictionary(List<HexDto> hexesList)
        {
            return hexesList.ToDictionary(
                hex => hex.HexId,
                hex => new HexModel(hex.HexId, hex.HexNumber, hex.FieldType, hex.Q, hex.R)
                );
        }

        public static Dictionary<int, PortModel> MapPortsDtoToDictionary(List<PortDto> portsList)
        {
            return portsList.ToDictionary(
                port => port.EdgeId,
                port => new PortModel(port.EdgeId, port.Type)
                );
        }

        public static List<(int, int, int)> MapCornersDtoToList(List<CornerDto> corners)
        {
            return corners.Select(corner => (corner.HexQ, corner.HexR, corner.CornerIndex)).ToList();
        }
    }
}
}
