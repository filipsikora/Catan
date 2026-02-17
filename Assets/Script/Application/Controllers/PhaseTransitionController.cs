using Catan.Application.Phases;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Controllers
{
    public sealed class PhaseTransitionController
    {
        private readonly EventBus _bus;
        private readonly Facade _facade;

        public BasePhase Current { get; set; }

        public PhaseTransitionController(Facade facade, EventBus bus)
        {
            _bus = bus;
            _facade = facade;
        }

        public void ChangePhase(EnumGamePhases next)
        {
            Current = CreatePhase(next);

            _bus.Publish(new PhaseChangedEvent(next));
        }

        private BasePhase CreatePhase(EnumGamePhases next)
        {
            return next switch
            {
                EnumGamePhases.BeforeRoll => new BeforeRollPhase(_facade, _bus, this),
                EnumGamePhases.FirstRoundsBuilding => new FirstRoundsBuildingPhase(_facade, _bus, this),
                EnumGamePhases.BankTrade => new BankTradePhase(_facade, _bus, this),
                EnumGamePhases.CardDiscarding => new CardDiscardingPhase(_facade, _bus, this),
                EnumGamePhases.CardStealing => new CardStealingPhase(_facade, _bus, this),
                EnumGamePhases.RobberPlacing => new RobberPlacingPhase(_facade, _bus, this),
                EnumGamePhases.DevelopmentCards => new DevelopmentCardsPhase(_facade, _bus, this),
                EnumGamePhases.MonopolyCard => new MonopolyCardPhase(_facade, _bus, this),
                EnumGamePhases.NormalRound => new NormalRoundPhase(_facade, _bus, this),
                EnumGamePhases.RoadBuilding => new RoadBuildingPhase(_facade, _bus, this),
                EnumGamePhases.TradeOffer => new TradeOfferPhase(_facade, _bus, this),
                EnumGamePhases.TradeRequest => new TradeRequestPhase(_facade, _bus, this),
                EnumGamePhases.YearOfPlentyCard => new YearOfPlentyCardPhase(_facade, _bus, this)
            };
        }
    }
}