using Catan.Shared.Communication.Events;
using Catan.Unity.Data;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRoadBuilding : BasePhaseAdapter
    {
        private BinderNormalRound _binder;

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, false);
            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);


            Manager.EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);

            Manager.EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            Manager.EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

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
            var edge = Manager.Game.Map.GetEdgeById(signal.EdgeId);
            var (_, _, mid) = Manager.BoardVisuals.GetEdgePositions(edge);
            var rotation = Manager.BoardVisuals.GetEdgeRotation(edge);
            var playerColor = RegistryPlayerColor.GetColor(Manager.Game.CurrentPlayer.ID);

            Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
        }

        public override void OnExit()
        {
            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, true);
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);

            Manager.EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);

            Manager.EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            Manager.EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);
        }
    }
}