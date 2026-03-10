using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Unity.Helpers;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterFirstRoundsBuilding : BasePhaseAdapter
    {
        public BinderFirstRoundBuildings _binder;
        private TurnDataSnapshot _turnDataSnapshot;

        public AdapterFirstRoundsBuilding(ManagerUI ui, EventBus bus, Facade facade, HandlerEvents eventsHandler) : base(ui, bus, facade, eventsHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderFirstRoundBuildings(UI, EventBus, EventsHandler);
            _binder.Bind();

            _turnDataSnapshot = Facade.GetTurnData();

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            UI.UpdateTurnCounter(_turnDataSnapshot.TurnNumber);

            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UI.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(signal.CanBuildVillage);
            UI.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(signal.CanBuildRoad);
        }

        private void OnVillagePlaced(VillagePlacedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new VillagePlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedUIEvent signal)
        {
            UI.MainUIPanel.NextTurnButton.gameObject.SetActive(true);

            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
            EventBus.Publish(new RoadPlacedUIEvent(signal.EdgeId, _turnDataSnapshot.PlayerId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PositionsResetUIEvent());

            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedUIEvent>(OnRoadPlaced);
        }
    }
}