using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace Catan
{
    public class NormalRound : GamePhase
    {
        private HandlerNormalRound _handler;
        private BinderNormalRound _binder;

        public override void OnEnter()
        {
            _handler = new HandlerNormalRound(Game, EventBus);
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            UI.UpdatePlayerInfo(Game.CurrentPlayer);
            VisualsUI.ShowNextTurnUI(UI.MainUIPanel);

            EventBus.Subscribe<TradeOfferPossibleSignal>(OnTradePossible);
            EventBus.Subscribe<TradeOfferConfirmedSignal>(OnTradeConfirmed);

            EventBus.Subscribe<RequestBankTradeSignal>(OnBankTradeRequested);

            EventBus.Subscribe<VertexHighlightedSignal>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedSignal>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsShownSignal>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedSignal>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedSignal>(OnRoadPlaced);
            EventBus.Subscribe<TownPlacedSignal>(OnTownPlaced);

            EventBus.Subscribe<TurnEndedSignal>(OnTurnEnded);

            EventBus.Subscribe<DevelopmentCardBoughtSignal>(OnDevelopmentCardBought);
            EventBus.Subscribe<DevelopmentCardsShownSignal>(OnDevelopmentCardsShown);
        }

        private void OnTradePossible(TradeOfferPossibleSignal signal)
        {
            if (signal.CanTrade)
            {
                UI.ShowTradeOfferButton();
            }
            else
            {
                UI.HideTradeOfferButton();
            }
        }

        private void OnTradeConfirmed(TradeOfferConfirmedSignal signal)
        {
            Handler.TransitionTo(new TradeOffer(signal.OfferedCards));
        }

        private void OnBankTradeRequested(RequestBankTradeSignal _)
        {
            Handler.TransitionTo(new BankTrade());
        }

        private void OnVertexClicked(VertexHighlightedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Manager.BoardVisuals.SetVertexVisual(vertexObject, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var edgeObject = Manager.BoardVisuals.GetEdgeObject(signal.EdgeId);
            Manager.BoardVisuals.SetEdgeVisual(edgeObject, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsShownSignal signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnVillagePlaced(VillagePlacedSignal signal)
        {
            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Vector3 pos = vertexObject.transform.position;
            var playerColor = Game.CurrentPlayer.PlayerColor;

            var villageObject = Manager.BoardVisuals.PlaceObject(Manager.CubeVillagePrefab, pos, null, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Game.CurrentPlayer);
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

        private void OnTownPlaced(TownPlacedSignal signal)
        {
            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Vector3 pos = vertexObject.transform.position;
            var playerColor = Game.CurrentPlayer.PlayerColor;

            var villageObject = Manager.BoardVisuals.PlaceObject(Manager.CubeTownPrefab, pos, null, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Game.CurrentPlayer);
        }

        private void OnTurnEnded(TurnEndedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();
            UI.HideTradeOfferButton();
            UI.UpdatePlayerInfo(Game.CurrentPlayer);
            UI.UpdateTurnCounter(Game.Turn);
        }

        private void OnDevelopmentCardBought(DevelopmentCardBoughtSignal signal)
        {
            UI.UpdatePlayerInfo(Game.CurrentPlayer);
        }

        private void OnDevelopmentCardsShown(DevelopmentCardsShownSignal signal)
        {
            Handler.TransitionTo(new DevelopmentCards(signal.AfterRoll));
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            EventBus.Unsubscribe<TradeOfferPossibleSignal>(OnTradePossible);
            EventBus.Unsubscribe<TradeOfferConfirmedSignal>(OnTradeConfirmed);

            EventBus.Unsubscribe<RequestBankTradeSignal>(OnBankTradeRequested);

            EventBus.Unsubscribe<VertexHighlightedSignal>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedSignal>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsShownSignal>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedSignal>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedSignal>(OnRoadPlaced);
            EventBus.Unsubscribe<TownPlacedSignal>(OnTownPlaced);

            EventBus.Unsubscribe<TurnEndedSignal>(OnTurnEnded);
        }
    }
}