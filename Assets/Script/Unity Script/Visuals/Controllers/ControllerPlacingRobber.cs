using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using UnityEngine;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerPlacingRobber
    {
        private VisualsBoard _board;
        private GameObject? _robber;
        private BoardManager _boardManager;

        public ControllerPlacingRobber(EventBus bus, VisualsBoard board, BoardManager boardManager)
        {
            _board = board;
            _boardManager = boardManager;

            bus.Subscribe<RobberMovedUIEvent>(OnRobberPlaced);
        }

        public void OnRobberPlaced(RobberMovedUIEvent signal)
        {
            var hexObject = _board.GetHexObject(signal.HexId);

            if (hexObject == null)
                return;

            var pos = hexObject.transform.position;

            if (_robber == null)
            {
                _robber = _board.PlaceObject(_boardManager.CubeRobberPrefab, pos, null, null, _boardManager.Board);
            }

            else
            {
                _board.MoveObject(_robber, pos);
            }
        }
    }
}