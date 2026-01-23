using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;

namespace Catan.Core.Phases.Handlers
{
    public class LogicNormalRound : BaseBuildPhaseLogic
    {
        private readonly ResourceCostOrStock _selected = new();

        private BuildVillageLogic _handlerVillage;
        private BuildRoadLogic _handlerRoad;
        private UpgradeVillageLogic _handlerTown;
        private BuyDevCardLogic _handlerBuyDevCard;
        private FinishTurnHandler _handlerTurn;

        public LogicNormalRound(GameState game, EventBus bus) : base(game, bus)
        {
            _handlerVillage = new BuildVillageLogic(game);
            _handlerRoad = new BuildRoadLogic(game);
            _handlerTown = new UpgradeVillageLogic(game);
            _handlerBuyDevCard = new BuyDevCardLogic(game);
            _handlerTurn = new FinishTurnHandler(game);
        }

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
                    Bus.Publish(new ProceedToDevelopmentCardsEvent());
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

            bool canTrade = _selected.Total() > 0;

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
            int playerId = Game.GetCurrentPlayer().ID;
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);
            var result = _handlerVillage.Handle(playerId, vertex);

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
            int playerId = Game.GetCurrentPlayer().ID;
            int id = SelectedEdgeId.Value;
            var edge = Game.Map.GetEdgeById(id);
            var result = _handlerRoad.Handle(playerId, edge);

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
            int playerId = Game.GetCurrentPlayer().ID;
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);
            var result = _handlerTown.Handle(playerId, vertex);

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
            if (_selected.Total() == 0)
                return;

            Bus.Publish(new NormalRoundToOfferTradeEvent(_selected));
        }

        private void HandleEndTurnRequested(EndTurnCommand signal)
        {
            var result = _handlerTurn.Handle(Game.GetCurrentPlayer());

            Bus.Publish(new NormalRoundToBeforeRollEvent());
        }

        private void HandleDevelopmentCardsBuyRequested(BuyDevelopmentCardCommand signal)
        {
            var playerId = Game.GetCurrentPlayer().ID;
            var result = _handlerBuyDevCard.Handle(playerId, Game.DevelopmentCardsDeckAvailable);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(playerId, result.Reason));
                return;
            }

            Bus.Publish(new DevelopmentCardBoughtEvent(result.DevCardId));
        }
    }
}