using Catan.Application.Queries.DevCards;
using Catan.Application.Snapshots;
using Catan.Core.Engine;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Application.Queries.Board
{
    public sealed class InMemoryBoardQueryServices : IBoardQueryService
    {
        private readonly GameState _game;

        public InMemoryBoardQueryServices(GameState game)
        {
            _game = game;
        }

        public EdgeSnapshot GetEdgeData(int edgeId)
        {
            var edge = _game.Map.GetEdgeById(edgeId);
            
            return new EdgeSnapshot(edgeId, edge.VertexA.Id, edge.VertexB.Id);
        }

        public VertexSnapshot GetVertexData(int vertexId)
        {
            var vertex = _game.Map.GetVertexById(vertexId);
            var corners = new List<(int HexQ, int HexR, int CornerIndex)>();

            foreach (var hex in vertex.AdjacentHexTiles)
            {
                var cornerIndex = hex.AdjacentVertices.IndexOf(vertex);

                if (cornerIndex >= 0)
                {
                    corners.Add((hex.Q, hex.R, cornerIndex));
                }
            }

            return new VertexSnapshot(vertexId, corners);
        }

        public HexSnapshot GetHexData(int hexId)
        {
            var hex = _game.Map.GetHexById(hexId);
            var hexNumber = hex.FieldNumber;
            var hexType = hex.FieldType;
            var hexQ = hex.Q;
            var hexR = hex.R;

            return new HexSnapshot(hexId, hexNumber, hexType, hexQ, hexR);
        }

        public PortSnapshot GetPortData(int edgeId)
        {
            var edge = _game.Map.GetEdgeById(edgeId);
            var port = _game.Map.PortList.First(p => p.Edge == edge);

            return new PortSnapshot(edge.Id, port.Type);
        }

        public BoardSnapshot GetBoardData()
        {
            var HexesSnapshotsList = new List<HexSnapshot>();
            var VerticesSnapshotsList = new List<VertexSnapshot>();
            var EdgesSnapshotsList = new List<EdgeSnapshot>();
            var PortsSnapshotList = new List<PortSnapshot>();

            foreach (var hex in _game.Map.HexList)
            {
                var hexData = GetHexData(hex.Id);
                HexesSnapshotsList.Add(hexData);
            }

            foreach (var vertex in _game.Map.VertexList)
            {
                var vertexData = GetVertexData(vertex.Id);
                VerticesSnapshotsList.Add(vertexData);
            }

            foreach (var edge in _game.Map.Edges)
            {
                var edgeData = GetEdgeData(edge.Id);
                EdgesSnapshotsList.Add(edgeData);
            }

            foreach (var port in _game.Map.PortList)
            {
                var edge = port.Edge;
                var portData = GetPortData(edge.Id);
                PortsSnapshotList.Add(portData);
            }

            return new BoardSnapshot(HexesSnapshotsList, VerticesSnapshotsList, EdgesSnapshotsList, PortsSnapshotList, _game.GetBlockedHexId());
        }
    }
}