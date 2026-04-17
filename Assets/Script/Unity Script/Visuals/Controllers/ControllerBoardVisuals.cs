using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using UnityEngine;

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
            _bus.Subscribe<VertexHighlightedUIEvent>(OnVertexHighlighted);
            _bus.Subscribe<EdgeHighlightedUIEvent>(OnEdgeClicked);
        }

        private void OnAllPositionsReset(PositionsResetUIEvent signal)
        {
            ResetAllPositions();
        }

        private void OnVertexHighlighted(VertexHighlightedUIEvent signal)
        {
            UnityEngine.Debug.Log($"vertex highlighted {signal.VertexId}");
            ResetAllPositions();

            var vertexObject = _board.GetVertexObject(signal.VertexId);
            _board.SetVertexVisual(vertexObject, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedUIEvent signal)
        {
            ResetAllPositions();

            var edgeObject = _board.GetEdgeObject(signal.EdgeId);
            _board.SetEdgeVisual(edgeObject, Color.yellow);
        }

        private void ResetAllPositions()
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