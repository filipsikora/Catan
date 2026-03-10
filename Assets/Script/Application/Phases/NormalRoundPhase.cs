using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Core.Models;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class NormalRoundPhase : BaseBuildPhase
    {
        private readonly ResourceCostOrStock _selected = new();

        public NormalRoundPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    return HandleResourceSelectionChanged(c);

                case VertexClickedCommand c:
                    return HandleVertexClicked(c);

                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildVillageCommand c:
                    return HandleVillageRequested(c);

                case BuildRoadCommand c:
                    return HandleRoadRequested(c);

                case UpgradeVillageCommand c:
                    return HandleTownRequested(c);

                case BankTradeCommand c:
                    return GameResult.Ok(EnumGamePhases.BankTrade);

                case OfferTradeCommand c:
                    return HandleTradeRequested(c);

                case EndTurnCommand c:
                    return HandleEndTurnRequested(c);

                case BuyDevelopmentCardCommand c:
                    return HandleDevelopmentCardsBuyRequested(c);

                case ShowDevelopmentCardsCommand c:
                    return GameResult.Ok(EnumGamePhases.DevelopmentCards);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceSelectionChanged(ResourceCardSelectedCommand signal)
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

            return GameResult.Ok().AddUIMessage(new SelectionChangedMessage(canTrade));
        }

        private GameResult HandleVertexClicked(VertexClickedCommand signal)
        {
            SelectedVertexId = signal.VertexId;
            SelectedEdgeId = null;

            var village = true;
            var road = false;
            var town = true;

            return GameResult.Ok().AddUIMessage(new VertexHighlightedMessage(signal.VertexId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));

        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            SelectedVertexId = null;
            SelectedEdgeId = signal.EdgeId;

            var village = false;
            var road = true;
            var town = false;

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));

        }

        private GameResult HandleVillageRequested(BuildVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var result = Facade.UseBuildVillage(id);

            ResetSelection();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }
            
            return GameResult.Ok().AddDomainEvent(new VillagePlacedEvent(id, Facade.GetCurrentPlayerId()));
        }

        private GameResult HandleRoadRequested(BuildRoadCommand signal)
        {
            int id = SelectedEdgeId.Value;
            var result = Facade.UseBuildRoad(id);

            ResetSelection();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEvent(new RoadPlacedEvent(id, Facade.GetCurrentPlayerId()));
        }

        private GameResult HandleTownRequested(UpgradeVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var result = Facade.UseUpgradeVillage(id);

            ResetSelection();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEvent(new TownPlacedEvent(id, Facade.GetCurrentPlayerId()));
        }

        private GameResult HandleTradeRequested(OfferTradeCommand signal)
        {
            int playerId = Facade.GetCurrentPlayerId();

            if (_selected.Total() == 0)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.InvalidSelection));

            var result = Facade.UsePrepareTrade(_selected);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }

        private GameResult HandleEndTurnRequested(EndTurnCommand signal)
        {
            var result = Facade.UseFinishTurn();

            return GameResult.Ok(result.NextPhase);
        }

        private GameResult HandleDevelopmentCardsBuyRequested(BuyDevelopmentCardCommand signal)
        {
            var result = Facade.UseBuyDevCard();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEvent(new DevelopmentCardBoughtEvent(result.DevCardId.Value));
        }
    }
}