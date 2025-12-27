using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Results;
using System.Linq;

namespace Catan.Core.Phases.Handlers
{
    public class LogicNormalRound : BaseBuildPhaseLogic
    {
        private readonly ResourceCostOrStock _selected = new();
        private bool _afterRoll = true;

        public LogicNormalRound(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceSelectionChanged(c);
                    break;

                case VertexClickedCommand c:
                    HandleVertexClicked(c);
                    break;

                case EdgeClickedCommand c:
                    HandleEdgeClicked(c);
                    break;

                case BuildVillageCommand c:
                    HandleVillageRequested(c);
                    break;

                case BuildRoadCommand c:
                    HandleRoadRequested(c);
                    break;

                case UpgradeVillageCommand c:
                    HandleTownRequested(c);
                    break;

                case BankTradeCommand c:
                    Bus.Publish(new NormalRoundToBankTradeEvent());
                    break;

                case OfferTradeCommand c:
                    HandleTradeRequested(c);
                    break;

                case EndTurnCommand c:
                    HandleEndTurnRequested(c);
                    break;

                case BuyDevelopmentCardCommand c:
                    HandleDevelopmentCardsBuyRequested(c);
                    break;

                case ShowDevelopmentCardsCommand c:
                    Bus.Publish(new ProceedToDevelopmentCardsEvent(Game.GetCurrentPlayerDevelopmentCardIds(), _afterRoll));
                    break;

                case RequestRolledNumberCommand c:
                    Bus.Publish(new DiceRolledEvent(Game.GetRolledNumber()));
                    break;
            }
        }

        private void HandleResourceSelectionChanged(ResourceCardSelectedCommand signal)
        {
            if (signal.IsSelected)
            {
                _selected.AddExactAmount(signal.Type, 1);
            }

            else
            {
                _selected.SubtractExactAmount(signal.Type, 1);
            }

            bool canTrade = _selected.ResourceDictionary.Values.Sum() > 0;

            Bus.Publish(new SelectionChangedEvent(canTrade));
        }

        private void HandleVertexClicked(VertexClickedCommand signal)
        {
            SelectedVertexId = signal.VertexId;
            SelectedEdgeId = null;

            var vertex = Game.Map.GetVertexById(signal.VertexId);
            (bool village, bool road, bool town) = Game.CheckBuildOptions(vertex);

            Bus.Publish(new VertexHighlightedEvent(signal.VertexId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleEdgeClicked(EdgeClickedCommand signal)
        {
            SelectedVertexId = null;
            SelectedEdgeId = signal.EdgeId;

            var edge = Game.Map.GetEdgeById(signal.EdgeId);
            (bool village, bool road, bool town) = Game.CheckBuildOptions(edge);

            Bus.Publish(new EdgeHighlightedEvent(signal.EdgeId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleVillageRequested(BuildVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);
            var result = Game.BuildVillage(Game.CurrentPlayer, vertex);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            Bus.Publish(new VillagePlacedEvent(id));
        }

        private void HandleRoadRequested(BuildRoadCommand signal)
        {
            int id = SelectedEdgeId.Value;
            var edge = Game.Map.GetEdgeById(id);
            var result = Game.BuildRoad(Game.CurrentPlayer, edge);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            Bus.Publish(new RoadPlacedEvent(id));
        }

        private void HandleTownRequested(UpgradeVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);
            var result = Game.UpgradeVillage(Game.CurrentPlayer, vertex);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            Bus.Publish(new TownPlacedEvent(id));
        }

        private void HandleTradeRequested(OfferTradeCommand signal)
        {
            if (_selected.ResourceDictionary.Values.Sum() == 0)
                return;

            Bus.Publish(new NormalRoundToOfferTradeEvent(_selected));
        }

        private void HandleEndTurnRequested(EndTurnCommand signal)
        {
            Game.EndTurn();
            Game.SetAfterRollTo(false);

            foreach (int id in Game.CurrentPlayer.DevelopmentCardsByID)
            {
                DevelopmentCard card = Game.DevelopmentCardsDeckAll.Find(c => c.ID == id);
                card.IsNew = false;
            }

            Bus.Publish(new NormalRoundToBeforeRollEvent());
        }

        private void HandleDevelopmentCardsBuyRequested(BuyDevelopmentCardCommand signal)
        {
            var result = Game.BuyDevelopmentCard(Game.CurrentPlayer);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            var card = result.Value;
            Bus.Publish(new DevelopmentCardBoughtEvent(card.ID));
        }
    }
}
