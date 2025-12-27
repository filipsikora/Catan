using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;
using static Catan.Shared.Communication.Events.ResourcesAvailabilityEvent;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterNormalRound : BasePhaseAdapter
    {
        private BinderNormalRound _binder;

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.UpdatePlayerInfo(Manager.Game.GetCurrentPlayer());
            VisualsUI.ShowNextTurnUI(UI.MainUIPanel);
            

            EventBus.Subscribe<SelectionChangedEvent>(OnTradePossible);

            EventBus.Subscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);
            EventBus.Subscribe<TownPlacedEvent>(OnTownPlaced);

            EventBus.Subscribe<DevelopmentCardBoughtEvent>(OnDevelopmentCardBought);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Subscribe<DiceRolledEvent>(OnDiceRolled);

            UI.HideTradeOfferButton();
            VisualsUI.ResetResourceCardsInParent(UI.PlayerUIPanel.ResourceCardsPanel);

            EventBus.Publish(new RequestRolledNumberCommand());
        }

        private void OnDiceRolled(DiceRolledEvent signal)
        {
            UI.MainUIPanel.UpdateRolledDice(signal.RolledNumber);
        }

        private void OnTradePossible(SelectionChangedEvent signal)
        {
            if (signal.ActionAvailable)
            {
                UI.ShowTradeOfferButton();
            }
            else
            {
                UI.HideTradeOfferButton();
            }
        }

        private void OnVertexClicked(VertexHighlightedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Manager.BoardVisuals.SetVertexVisual(vertexObject, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var edgeObject = Manager.BoardVisuals.GetEdgeObject(signal.EdgeId);
            Manager.BoardVisuals.SetEdgeVisual(edgeObject, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnVillagePlaced(VillagePlacedEvent signal)
        {
            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Vector3 pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(Manager.Game.CurrentPlayer.ID);

            var villageObject = Manager.BoardVisuals.PlaceObject(Manager.CubeVillagePrefab, pos, null, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            var edge = Manager.Game.Map.GetEdgeById(signal.EdgeId);
            var (_, _, mid) = Manager.BoardVisuals.GetEdgePositions(edge);
            var rotation = Manager.BoardVisuals.GetEdgeRotation(edge);
            var playerColor = RegistryPlayerColor.GetColor(Manager.Game.CurrentPlayer.ID);

            Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
        }

        private void OnTownPlaced(TownPlacedEvent signal)
        {
            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Vector3 pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(Manager.Game.CurrentPlayer.ID);

            var villageObject = Manager.BoardVisuals.PlaceObject(Manager.CubeTownPrefab, pos, null, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
        }

        private void OnDevelopmentCardBought(DevelopmentCardBoughtEvent signal)
        {
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            var card = Manager.ControllerResourceCardsUI.GetVisualResourceCardById(signal.VisualResourceCardId);

            if (card == null)
                return;

            EventBus.Publish(new ResourceCardSelectedCommand(!card.IsToggled, card.Type));

            if (card.IsToggled)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            card.ToggleCard();
        }

        public override void OnExit()
        {
            _binder.Unbind();

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.HideTradeOfferButton();
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
            UI.UpdateTurnCounter(Manager.Game.Turn);

            EventBus.Unsubscribe<SelectionChangedEvent>(OnTradePossible);

            EventBus.Unsubscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);
            EventBus.Unsubscribe<TownPlacedEvent>(OnTownPlaced);

            EventBus.Unsubscribe<DevelopmentCardBoughtEvent>(OnDevelopmentCardBought);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }
    }
}