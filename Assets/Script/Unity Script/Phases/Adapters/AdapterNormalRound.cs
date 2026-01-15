using Catan.Application.Snapshots;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;
using Core.Unity.Communication.InternalUIEvents;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterNormalRound : BasePhaseAdapter
    {
        private BinderNormalRound _binder;
        private TurnDataSnapshot _turnDataSnapshot;

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            _turnDataSnapshot = Manager.TurnsQueryService.GetTurnData();            

            EventBus.Subscribe<SelectionChangedEvent>(OnTradePossible);

            EventBus.Subscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);
            EventBus.Subscribe<TownPlacedEvent>(OnTownPlaced);

            EventBus.Subscribe<DevelopmentCardBoughtEvent>(OnDevelopmentCardBought);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.HideTradeOfferButton();
            VisualsUI.ResetResourceCardsInParent(UI.PlayerUIPanel.ResourceCardsPanel);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            VisualsUI.ShowNextTurnUI(UI.MainUIPanel);
            UI.MainUIPanel.UpdateRolledDice(_turnDataSnapshot.RolledNumber);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
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
            Manager.BoardVisuals.ResetMarkedPositions();

            EventBus.Publish(new VillagePlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            EventBus.Publish(new RoadPlacedUIEvent(signal.EdgeId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnTownPlaced(TownPlacedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            EventBus.Publish(new TownPlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnDevelopmentCardBought(DevelopmentCardBoughtEvent signal)
        {
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
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
            UI.UpdateTurnCounter(_turnDataSnapshot.TurnNumber);

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