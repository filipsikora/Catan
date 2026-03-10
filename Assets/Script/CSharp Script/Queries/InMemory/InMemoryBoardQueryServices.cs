using Catan.Core.Snapshots;
using Catan.Core.Queries.Interfaces;
using System.Collections.Generic;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryBoardQueryServices : IBoardQueryService
    {
        private readonly GameSession _session;

        public InMemoryBoardQueryServices(GameSession session)
        {
            _session = session;
        }

        public EdgeSnapshot GetEdgeData(int edgeId)
        {
            var edge = _session.GetEdgeById(edgeId);
            
            return new EdgeSnapshot(edgeId, edge.VertexA.Id, edge.VertexB.Id);
        }

        public VertexSnapshot GetVertexData(int vertexId)
        {
            var vertex = _session.GetVertexById(vertexId);
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
            var hex = _session.GetHexById(hexId);
            var hexNumber = hex.FieldNumber;
            var hexType = hex.FieldType;
            var hexQ = hex.Q;
            var hexR = hex.R;

            return new HexSnapshot(hexId, hexNumber, hexType, hexQ, hexR);
        }

        public PortSnapshot GetPortData(int edgeId)
        {
            var edge = _session.GetEdgeById(edgeId);
            var port = _session.GetPortByEdge(edge);

            return new PortSnapshot(edge.Id, port.Type);
        }

        public BoardSnapshot GetBoardData()
        {
            var HexesSnapshotsList = new List<HexSnapshot>();
            var VerticesSnapshotsList = new List<VertexSnapshot>();
            var EdgesSnapshotsList = new List<EdgeSnapshot>();
            var PortsSnapshotList = new List<PortSnapshot>();

            foreach (var hex in _session.GetAllHexTilesView())
            {
                var hexData = GetHexData(hex.Id);
                HexesSnapshotsList.Add(hexData);
            }

            foreach (var vertex in _session.GetAllVerticesView())
            {
                var vertexData = GetVertexData(vertex.Id);
                VerticesSnapshotsList.Add(vertexData);
            }

            foreach (var edge in _session.GetAllEdgesView())
            {
                var edgeData = GetEdgeData(edge.Id);
                EdgesSnapshotsList.Add(edgeData);
            }

            foreach (var port in _session.GetAllPortsView())
            {
                var edge = port.Edge;
                var portData = GetPortData(edge.Id);
                PortsSnapshotList.Add(portData);
            }

            return new BoardSnapshot(HexesSnapshotsList, VerticesSnapshotsList, EdgesSnapshotsList, PortsSnapshotList, _session.GetBlockedHexId());
        }
    }
}