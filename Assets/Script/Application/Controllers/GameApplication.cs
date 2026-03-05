#nullable enable
using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.Phases;
using Catan.Application.UIMessages;
using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Application
{
    public class GameApplication
    {
        public BasePhase? Current { get; set; }
        public Facade Facade { get; }

        public GameApplication(Facade facade)
        {
            Facade = facade;
            Current = null;
        }

        public GameResult Execute(ICommand command)
        {
            var result = Current.Handle(command);

            if (result.NextPhase != null)
            {
                var nextPhase = result.NextPhase.Value;
                Current = CreateApplicationPhase(nextPhase);
                IUIMessages? UImessages = Current.Enter();

                if (UImessages != null)
                    result.AddUIMessage(UImessages);

                result.AddUIMessage(new PhaseChangedMessage(nextPhase));
            }

            return result;
        }

        private BasePhase CreateApplicationPhase(EnumGamePhases phase)
        {
            return phase switch
            {
                EnumGamePhases.BeforeRoll => new BeforeRollPhase(Facade),
                EnumGamePhases.FirstRoundsBuilding => new FirstRoundsBuildingPhase(Facade),
                EnumGamePhases.BankTrade => new BankTradePhase(Facade),
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