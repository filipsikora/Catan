using Catan.Application.Snapshots;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRoadBuilding : BasePhaseAdapter
    {
        private BinderNormalRound _binder;
        private TurnDataSnapshot _turnDataSnapshot;

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            _turnDataSnapshot = Manager.TurnsQueryService.GetTurnData();

            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, false);
            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);


            Manager.EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);

            Manager.EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            Manager.EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var edgeObj = Manager.BoardVisuals.GetEdgeObject(signal.EdgeId);
            Manager.BoardVisuals.SetEdgeVisual(edgeObj, Color.yellow);
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

            Manager.EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);

            Manager.EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            Manager.EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);
        }
    }
}