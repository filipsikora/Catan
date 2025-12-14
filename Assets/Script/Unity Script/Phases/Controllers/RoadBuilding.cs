using Catan.Catan;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Catan.Core;
using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class RoadBuilding : GamePhase
    {
        private HandlerRoadBuilding _handler;
        private BinderNormalRound _binder;

        public override void OnEnter()
        {
            _handler = new HandlerRoadBuilding(Game, EventBus);
            _binder = new BinderNormalRound(UI, EventBus);

            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, false);
            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            EventBus.Subscribe<RoadPlacedSignal>(OnRoadPlaced);

            EventBus.Subscribe<RoadBuildingFinishedSignal>(OnRoadBuildingFinished);

            Manager.EventBus.Subscribe<EdgeHighlightedSignal>(OnEdgeClicked);
            Manager.EventBus.Subscribe<BuildOptionsShownSignal>(OnPositionClicked);
        }

        private void OnEdgeClicked(EdgeHighlightedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var edgeObj = Manager.BoardVisuals.GetEdgeObject(signal.EdgeId);
            Manager.BoardVisuals.SetEdgeVisual(edgeObj, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsShownSignal signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnRoadPlaced(RoadPlacedSignal signal)
        {
            var edge = Game.Map.GetEdgeById(signal.EdgeId);
            var (_, _, mid) = Manager.BoardVisuals.GetEdgePositions(edge);
            var rotation = Manager.BoardVisuals.GetEdgeRotation(edge);
            var color = Game.CurrentPlayer.PlayerColor;

            Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, color, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Game.CurrentPlayer);
        }

        private void OnRoadBuildingFinished(RoadBuildingFinishedSignal signal)
        {
            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, true);
            UI.UpdatePlayerInfo(Game.CurrentPlayer);

            Handler.TransitionTo(new NormalRound());
        }
    }
}