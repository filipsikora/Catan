using Catan.Application.Controllers;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class NormalRoundPhase : BaseBuildPhase
    {
        private readonly ResourceCostOrStock _selected = new();

        public NormalRoundPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

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
                    PhaseTransition.ChangePhase(EnumGamePhases.BankTrade);
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
                    PhaseTransition.ChangePhase(EnumGamePhases.DevelopmentCards);
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

            var village = true;
            var road = false;
            var town = true;

            Bus.Publish(new VertexHighlightedEvent(signal.VertexId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleEdgeClicked(EdgeClickedCommand signal)
        {
            SelectedVertexId = null;
            SelectedEdgeId = signal.EdgeId;

            var village = false;
            var road = true;
            var town = false;

            Bus.Publish(new EdgeHighlightedEvent(signal.EdgeId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleVillageRequested(BuildVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var result = Facade.UseBuildVillage(id);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.PlayerId, result.Reason));
                return;
            }
            
            Bus.Publish(new VillagePlacedEvent(id));
        }

        private void HandleRoadRequested(BuildRoadCommand signal)
        {
            int id = SelectedEdgeId.Value;
            var result = Facade.UseBuildRoad(id);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.PlayerId, result.Reason));
                return;
            }

            Bus.Publish(new RoadPlacedEvent(id));
        }

        private void HandleTownRequested(UpgradeVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var result = Facade.UseUpgradeVillage(id);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.PlayerId, result.Reason));
                return;
            }

            Bus.Publish(new TownPlacedEvent(id));
        }

        private void HandleTradeRequested(OfferTradeCommand signal)
        {
            if (_selected.Total() == 0)
                return;

            int playerId = Facade.GetCurrentPlayerId();
            var result = Facade.UsePrepareTrade(_selected);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(playerId, result.Reason));
                return;
            }

            PhaseTransition.ChangePhase(EnumGamePhases.TradeOffer);
        }

        private void HandleEndTurnRequested(EndTurnCommand signal)
        {
            var result = Facade.UseFinishTurn();

            PhaseTransition.ChangePhase(EnumGamePhases.BeforeRoll);
        }

        private void HandleDevelopmentCardsBuyRequested(BuyDevelopmentCardCommand signal)
        {
            var result = Facade.UseBuyDevCard();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.PlayerId, result.Reason));
                return;
            }

            Bus.Publish(new DevelopmentCardBoughtEvent(result.DevCardId.Value));
        }
    }
}