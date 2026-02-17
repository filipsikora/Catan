using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRoadBuilding : BasePhaseAdapter
    {
        private BinderNormalRound _binder;
        private TurnDataSnapshot _turnDataSnapshot;

        private VisualsBoard _board;

        public AdapterRoadBuilding(ManagerUI ui, EventBus bus, Facade facade, VisualsBoard board) : base(ui, bus, facade) { }

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            _turnDataSnapshot = Facade.GetTurnData();

            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, false);
            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);


            EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);

            EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var edgeObj = _board.GetEdgeObject(signal.EdgeId);
            _board.SetEdgeVisual(edgeObj, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new RoadPlacedUIEvent(signal.EdgeId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        public override void OnExit()
        {
            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, true);

            EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);

            EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);
        }
    }
}