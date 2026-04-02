using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Shared.Commands;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterFirstRoundsBuilding : BasePhaseAdapter
    {
        public BinderFirstRoundBuildings _binder;

        public AdapterFirstRoundsBuilding(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderFirstRoundBuildings(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<VertexClickedUIEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);
        }

        private void OnVertexClicked(VertexClickedUIEvent signal)
        {
            EventsHandler.Execute(new VertexClickedCommand(signal.VertexId));
        }

        private void OnEdgeClicked(EdgeClickedUIEvent signal)
        {
            EventsHandler.Execute(new EdgeClickedCommand(signal.EdgeId));
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
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