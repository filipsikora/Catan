using Catan.Unity.Helpers;
using Catan.Unity.Communication.InternalUIEvents;
using UnityEngine;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerPlacingRobber
    {
        private VisualsBoard _board;
        private GameObject? _robber;

        public ControllerPlacingRobber(EventBus bus, VisualsBoard board)
        {
            _board = board;

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
                _robber = _board.PlaceObject(ManagerGame.Instance.CubeRobberPrefab, pos, null, null, ManagerGame.Instance.Board);
            }

            else
            {
                _board.MoveObject(_robber, pos);
            }
        }
    }
}