using Catan.Shared.Data;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterFirstRoundsBuilding : BasePhaseAdapter
    {
        public BinderFirstRoundBuildings _binder;

        public AdapterFirstRoundsBuilding(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

        public override void OnEnter()
        {
            UnityEngine.Debug.Log("entered phase");
            UnityEngine.Debug.Log($"MainUI active: {UI.MainUIPanel.gameObject.activeSelf}");
            UnityEngine.Debug.Log($"PlayerUI active: {UI.PlayerUIPanel.gameObject.activeSelf}");

            
            EventBus.Publish(new PlayerStateChangedUIEvent(1));

            UnityEngine.Debug.Log("chuj");

            _binder = new BinderFirstRoundBuildings(UI, EventBus, EventsHandler);
            _binder.Bind();

            UI.MainUIPanel.gameObject.SetActive(true);
            UI.PlayerUIPanel.gameObject.SetActive(true);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);

            //VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<VertexClickedUIEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);
        }

        private void OnVertexClicked(VertexClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.VertexClickedCommand, new { vertexId = signal.VertexId });
        }

        private void OnEdgeClicked(EdgeClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.EdgeClickedCommand, new { edgeId = signal.EdgeId });
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UnityEngine.Debug.Log($"uievent received, road = {signal.CanBuildRoad}, village = {signal.CanBuildVillage}, town = {signal.CanUpgradeVillage}");
            UI.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(signal.CanBuildVillage);
            UI.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(signal.CanBuildRoad);
        }

        private void OnRoadPlaced(RoadPlacedUIEvent signal)
        {
            UI.MainUIPanel.NextTurnButton.gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PositionsResetUIEvent());

            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Unsubscribe<RoadPlacedUIEvent>(OnRoadPlaced);
        }
    }
}