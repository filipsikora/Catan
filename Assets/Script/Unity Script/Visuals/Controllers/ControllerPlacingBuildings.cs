using Catan.Unity.Helpers;
using Catan.Unity.Data;
using Catan.Unity.Communication.InternalUIEvents;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerPlacingBuildings
    {
        private VisualsBoard _board;
        public ControllerPlacingBuildings(EventBus bus, VisualsBoard board)
        {
            _board = board;

            bus.Subscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            bus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);
            bus.Subscribe<TownPlacedUIEvent>(OnTownPlaced);
        }

        public void OnVillagePlaced(VillagePlacedUIEvent signal)
        {
            var vertexObject = _board.GetVertexObject(signal.VertexId);
            var pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(signal.OwnerId);

            _board.PlaceObject(ManagerGame.Instance.CubeVillagePrefab, pos, null, playerColor, ManagerGame.Instance.Board);
        }

        public void OnRoadPlaced(RoadPlacedUIEvent signal)
        {
            var (a, b, mid, direction, rotation) = _board.GetEdgeVisualData(signal.EdgeId);
            var playerColor = RegistryPlayerColor.GetColor(signal.OwnerId);

            _board.PlaceObject(ManagerGame.Instance.CubeRoadPrefab, mid, rotation, playerColor, ManagerGame.Instance.Board);
        }

        public void OnTownPlaced(TownPlacedUIEvent signal)
        {
            var vertexObject = _board.GetVertexObject(signal.VertexId);
            var pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(signal.OwnerId);

            _board.PlaceObject(ManagerGame.Instance.CubeTownPrefab, pos, null, playerColor, ManagerGame.Instance.Board);
        }
    }
}