using Catan.Unity.Helpers;
using Catan.Unity.Communication.InternalUIEvents;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerBoardVisuals
    {
        private readonly EventBus _bus;
        private readonly VisualsBoard _board;

        public ControllerBoardVisuals(EventBus bus, VisualsBoard board)
        {
            _bus = bus;
            _board = board;

            _bus.Subscribe<PositionsResetUIEvent>(OnAllPositionsReset);
        }

        public void OnAllPositionsReset(PositionsResetUIEvent signal)
        {
            foreach (var vertexId in _board.GetVerticesIds())
            {
                var vertexObject = _board.GetVertexObject(vertexId);

                _board.ResetMarkedVertex(vertexObject);
            }

            foreach (var edgeId in _board.GetEdgesIds())
            {
                var edgeObject = _board.GetEdgeObject(edgeId);

                _board.ResetMarkedEdge(edgeObject);
            }
        }
    }
}