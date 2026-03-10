#nullable enable
using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.Phases;
using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Application
{
    public class GameApplication
    {
        public BasePhase Current { get; set; }
        public Facade Facade { get; }

        public GameApplication(Facade facade)
        {
            Facade = facade;
            Current = CreateApplicationPhase(EnumGamePhases.FirstRoundsBuilding);
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
                EnumGamePhases.CardDiscarding => new CardDiscardingPhase(Facade),
                EnumGamePhases.CardStealing => new CardStealingPhase(Facade),
                EnumGamePhases.RobberPlacing => new RobberPlacingPhase(Facade),
                EnumGamePhases.DevelopmentCards => new DevelopmentCardsPhase(Facade),
                EnumGamePhases.MonopolyCard => new MonopolyCardPhase(Facade),
                EnumGamePhases.NormalRound => new NormalRoundPhase(Facade),
                EnumGamePhases.RoadBuilding => new RoadBuildingPhase(Facade),
                EnumGamePhases.TradeOffer => new TradeOfferPhase(Facade),
                EnumGamePhases.TradeRequest => new TradeRequestPhase(Facade),
                EnumGamePhases.YearOfPlentyCard => new YearOfPlentyCardPhase(Facade)
            };
        }
    }
}