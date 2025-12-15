using Catan.Catan;
using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core
{
    public class HandlerNormalRound : BaseBuildHandler
    {
        private readonly ResourceCostOrStock _selected = new();
        private bool _afterRoll = true;
        private readonly List<int> _selectedVisualResourceCardsIds = new();

        public HandlerNormalRound(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<ResourceCardClickedSignal>(OnCardClicked);

            Bus.Subscribe<RequestOfferTradeSignal>(OnOfferTrade);

            Bus.Subscribe<VertexClickedSignal>(OnVertexClicked);
            Bus.Subscribe<EdgeClickedSignal>(OnEdgeClicked);

            Bus.Subscribe<RequestBuildVillageSignal>(OnVillageRequested);
            Bus.Subscribe<RequestBuildRoadSignal>(OnRoadRequested);
            Bus.Subscribe<RequestUpgradeVillageSignal>(OnTownRequested);

            Bus.Subscribe<RequestEndTurnSignal>(OnEndTurnRequested);

            Bus.Subscribe<RequestBuyDevelopmentCardSignal>(OnDevelopmentCardsBuyRequested);
            Bus.Subscribe<RequestShowDevelopmentCardsSignal>(OnDevelopmentCardsShowRequested);
        }

        private void OnCardClicked(ResourceCardClickedSignal signal)
        {
            var type = signal.Type;

            if (!signal.IsLeftClicked)
                return;

            if (!_selectedVisualResourceCardsIds.Contains(signal.VisualResourceCardId))
            {
                _selected.AddSingleType(type, 1);
                _selectedVisualResourceCardsIds.Add(signal.VisualResourceCardId);
                Bus.Publish(new ResourceCardVisualStateChangedSignal(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.Lifted));
            }

            else
            {
                _selected.SubtractSingleType(type, 1);
                _selectedVisualResourceCardsIds.Remove(signal.VisualResourceCardId);
                Bus.Publish(new ResourceCardVisualStateChangedSignal(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.None));
            }

            bool canTrade = _selected.ResourceDictionary.Values.Sum() > 0;

            Bus.Publish(new TradeOfferPossibleSignal(canTrade));
        }

        private void OnVertexClicked(VertexClickedSignal signal)
        {
            SelectedVertexId = signal.VertexId;
            SelectedEdgeId = null;

            var vertex = Game.Map.GetVertexById(signal.VertexId);
            (bool village, bool road, bool town) = Game.CheckBuildOptions(vertex);

            Bus.Publish(new VertexHighlightedSignal(signal.VertexId));
            Bus.Publish(new BuildOptionsShownSignal(village, road, town));
        }

        private void OnEdgeClicked(EdgeClickedSignal signal)
        {
            SelectedVertexId = null;
            SelectedEdgeId = signal.EdgeId;

            var edge = Game.Map.GetEdgeById(signal.EdgeId);
            (bool village, bool road, bool town) = Game.CheckBuildOptions(edge);

            Bus.Publish(new EdgeHighlightedSignal(signal.EdgeId));
            Bus.Publish(new BuildOptionsShownSignal(village, road, town));
        }

        private void OnVillageRequested(RequestBuildVillageSignal signal)
        {
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);

            bool success = Game.BuildVillage(Game.CurrentPlayer, vertex);

            if (!success)
            {
                ResetSelection();
                return;
            }

            Bus.Publish(new VillagePlacedSignal(id));
            ResetSelection();
        }

        private void OnRoadRequested(RequestBuildRoadSignal signal)
        {
            int id = SelectedEdgeId.Value;
            var edge = Game.Map.GetEdgeById(id);

            bool success = Game.BuildRoad(Game.CurrentPlayer, edge);

            if (!success)
            {
                ResetSelection();
                return;
            }

            Bus.Publish(new RoadPlacedSignal(id));
            ResetSelection();
        }

        private void OnTownRequested(RequestUpgradeVillageSignal signal)
        {
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);

            bool success = Game.UpgradeVillage(Game.CurrentPlayer, vertex);

            if (!success)
            {
                ResetSelection();
                return;
            }

            Bus.Publish(new TownPlacedSignal(id));
            ResetSelection();
        }


        private void OnOfferTrade(RequestOfferTradeSignal signal)
        {
            if (_selected.ResourceDictionary.Values.Sum() == 0)
                return;

            Bus.Publish(new TradeOfferConfirmedSignal(_selected));
        }

        private void OnEndTurnRequested(RequestEndTurnSignal signal)
        {
            Game.EndTurn();

            foreach (int id in Game.CurrentPlayer.DevelopmentCardsByID)
            {
                DevelopmentCard card = Game.DevelopmentCardsDeckAll.Find(c => c.ID == id);
                card.IsNew = false;
            }

            Bus.Publish(new TurnEndedSignal());
        }

        private void OnDevelopmentCardsBuyRequested(RequestBuyDevelopmentCardSignal signal)
        {
            DevelopmentCard card = Game.BuyDevelopmentCard(Game.CurrentPlayer);

            if (card == null)
                return;

            Bus.Publish(new DevelopmentCardBoughtSignal(card.ID));
        }

        private void OnDevelopmentCardsShowRequested(RequestShowDevelopmentCardsSignal signal)
        {
            Bus.Publish(new DevelopmentCardsShownSignal(Game.CurrentPlayer.DevelopmentCardsByID, _afterRoll));
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnCardClicked);
            Bus.Unsubscribe<RequestOfferTradeSignal>(OnOfferTrade);

            Bus.Unsubscribe<VertexClickedSignal>(OnVertexClicked);
            Bus.Unsubscribe<EdgeClickedSignal>(OnEdgeClicked);

            Bus.Unsubscribe<RequestBuildVillageSignal>(OnVillageRequested);
            Bus.Unsubscribe<RequestBuildRoadSignal>(OnRoadRequested);
            Bus.Unsubscribe<RequestUpgradeVillageSignal>(OnTownRequested);

            Bus.Unsubscribe<RequestEndTurnSignal>(OnEndTurnRequested);

            Bus.Unsubscribe<RequestBuyDevelopmentCardSignal>(OnDevelopmentCardsBuyRequested);
            Bus.Unsubscribe<RequestShowDevelopmentCardsSignal>(OnDevelopmentCardsShowRequested);
        }
    }
}
