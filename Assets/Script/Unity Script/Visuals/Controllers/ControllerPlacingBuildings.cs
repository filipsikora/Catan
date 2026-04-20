using Catan.Unity.Helpers;
using Catan.Unity.Data;
using Catan.Unity.InternalUIEvents;
using UnityEngine;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerPlacingBuildings
    {
        private VisualsBoard _boardVisuals;
        private Transform _board;

        private GameObject _villagePrefab;
        private GameObject _roadPrefab;
        private GameObject _townPrefab;

        private EventBus _bus;

        public ControllerPlacingBuildings(EventBus bus, VisualsBoard boardVisuals, Transform board, GameObject villagePrefab, GameObject roadPrefab, GameObject townPrefab)
        {
            _boardVisuals = boardVisuals;
            _board = board;

            _villagePrefab = villagePrefab;
            _roadPrefab = roadPrefab;
            _townPrefab = townPrefab;

            _bus = bus;

            bus.Subscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            bus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);
            bus.Subscribe<TownPlacedUIEvent>(OnTownPlaced);
        }

        public void OnVillagePlaced(VillagePlacedUIEvent signal)
        {
            var vertexObject = _boardVisuals.GetVertexObject(signal.VertexId);
            var pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(signal.OwnerId);

            _boardVisuals.PlaceObject(_villagePrefab, pos, null, playerColor, _board);

            _bus.Publish(new PositionsResetUIEvent());
        }

        public void OnRoadPlaced(RoadPlacedUIEvent signal)
        {
            var (a, b, mid, direction, rotation) = _boardVisuals.GetEdgeVisualData(signal.EdgeId);
            var playerColor = RegistryPlayerColor.GetColor(signal.OwnerId);

            _boardVisuals.PlaceObject(_roadPrefab, mid, rotation, playerColor, _board);

            _bus.Publish(new PositionsResetUIEvent());
        }

        public void OnTownPlaced(TownPlacedUIEvent signal)
        {
            var vertexObject = _boardVisuals.GetVertexObject(signal.VertexId);
            var pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(signal.OwnerId);
            
            _boardVisuals.PlaceObject(_townPrefab, pos, null, playerColor, _board);

            _bus.Publish(new PositionsResetUIEvent());
        }
    }
}