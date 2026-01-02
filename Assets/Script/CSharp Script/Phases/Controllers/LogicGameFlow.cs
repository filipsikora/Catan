using Catan.Core.Engine;
using Catan.Core.Phases.Handlers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Controllers
{
    public sealed class LogicGameFlow
    {
        private readonly GameState _game;
        private readonly LogicPhaseTransition _phasesTransition;
        private readonly EventBus _bus;

        public LogicGameFlow(GameState game, LogicPhaseTransition phasesTransition, EventBus bus)
        {
            _game = game;
            _phasesTransition = phasesTransition;
            _bus = bus;

            _bus.Subscribe<ReturnToNormalRoundEvent>(OnNormalRoundEntered);
            _bus.Subscribe<NormalRoundToBeforeRollEvent>(OnBeforeRollEntered);
            _bus.Subscribe<NormalRoundToOfferTradeEvent>(OnTradeOfferEntered);
            _bus.Subscribe<NormalRoundToBankTradeEvent>(OnBankTradeEntered);
            _bus.Subscribe<ProceedToDevelopmentCardsEvent>(OnDevCardsEntered);
            _bus.Subscribe<ProceedToRobberPlacingEvent>(OnRobberPlacingEntered);
            _bus.Subscribe<TradeOfferToTradeRequestEvent>(OnTradeRequestEntered);
            _bus.Subscribe<DiceRollCompletedEvent>(OnDiceRollCompleted);
            _bus.Subscribe<CardStealingCompletedEvent>(OnCardStealingCompleted);
            _bus.Subscribe<RobberPlacingToCardStealingEvent>(OnVictimChosen);
            _bus.Subscribe<DevelopmentCardsCompletedEvent>(OnDevCardsCompleted);
            _bus.Subscribe<FirstRoundsBuildingCompletedEvent>(OnFirstRoundsBuildingCompleted);
            _bus.Subscribe<DevelopmentCardsToMonopolyCardEvent>(OnMonopoloyCardEntered);
            _bus.Subscribe<DevelopmentCardsToRoadBuildingEvent>(OnRoadBuildingEntered);
            _bus.Subscribe<DevelopmentCardsToYearOfPlentyUsedEvent>(OnYearOfPlentyEntered);

            _bus.Subscribe<GameInitializedEvent>(OnGameInitialized);
        }

        private void OnNormalRoundEntered(ReturnToNormalRoundEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicNormalRound(_game, _bus), EnumGamePhases.NormalRound);
        }

        private void OnBeforeRollEntered(NormalRoundToBeforeRollEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicBeforeRoll(_game, _bus), EnumGamePhases.BeforeRoll);
        }

        private void OnTradeOfferEntered(NormalRoundToOfferTradeEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicTradeOffer(_game, _bus, signal.OfferedCards), EnumGamePhases.TradeOffer);
        }

        private void OnBankTradeEntered(NormalRoundToBankTradeEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicBankTrade(_game, _bus), EnumGamePhases.BankTrade);
        }

        private void OnDevCardsEntered(ProceedToDevelopmentCardsEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicDevelopmentCards(_game, _bus), EnumGamePhases.DevelopmentCards);
        }

        private void OnTradeRequestEntered(TradeOfferToTradeRequestEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicTradeRequest(_game, _bus, signal.PlayerId, signal.CardsOffered, signal.CardsDesired), EnumGamePhases.TradeRequest);
        }

        private void OnRobberPlacingEntered(ProceedToRobberPlacingEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicRobberPlacing(_game, _bus), EnumGamePhases.RobberPlacing);
        }

        private void OnVictimChosen(RobberPlacingToCardStealingEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicCardStealing(_game, _bus, signal.VictimId), EnumGamePhases.CardStealing);
        }

        private void OnDiceRollCompleted(DiceRollCompletedEvent signal)
        {
            if (signal.RolledSeven)
            {
                _phasesTransition.ChangePhase(new LogicCardDiscarding(_game, _bus), EnumGamePhases.CardDiscarding);
            }

            else
            {
                _phasesTransition.ChangePhase(new LogicNormalRound(_game, _bus), EnumGamePhases.NormalRound);
            }
        }

        private void OnCardStealingCompleted(CardStealingCompletedEvent signal)
        {
            if (_game.GetAfterRoll())
            {
                _phasesTransition.ChangePhase(new LogicNormalRound(_game, _bus), EnumGamePhases.NormalRound);
            }

            else
            {
                _phasesTransition.ChangePhase(new LogicBeforeRoll(_game, _bus), EnumGamePhases.BeforeRoll);
            }
        }

        private void OnDevCardsCompleted(DevelopmentCardsCompletedEvent signal)
        {
            if (signal.AfterRoll)
            {
                _phasesTransition.ChangePhase(new LogicNormalRound(_game, _bus), EnumGamePhases.NormalRound);
            }

            else
            {
                _phasesTransition.ChangePhase(new LogicBeforeRoll(_game, _bus), EnumGamePhases.BeforeRoll);
            }
        }

        private void OnFirstRoundsBuildingCompleted(FirstRoundsBuildingCompletedEvent signal)
        {
            if (signal.FirstTurnsLeft)
            {
                _phasesTransition.ChangePhase(new LogicFirstRoundsBuilding(_game, _bus), EnumGamePhases.FirstRoundsBuilding);
            }

            else
            {
                _phasesTransition.ChangePhase(new LogicBeforeRoll(_game, _bus), EnumGamePhases.BeforeRoll);
            }
        }

        private void OnMonopoloyCardEntered(DevelopmentCardsToMonopolyCardEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicMonopolyCard(_game, _bus), EnumGamePhases.MonopolyCard);
        }

        private void OnRoadBuildingEntered(DevelopmentCardsToRoadBuildingEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicRoadBuilding(_game, _bus), EnumGamePhases.RoadBuilding);
        }

        private void OnYearOfPlentyEntered(DevelopmentCardsToYearOfPlentyUsedEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicYearOfPlentyCard(_game, _bus), EnumGamePhases.YearOfPlentyCard);
        }

        private void OnGameInitialized(GameInitializedEvent signal)
        {
            _phasesTransition.ChangePhase(new LogicFirstRoundsBuilding(_game, _bus), EnumGamePhases.FirstRoundsBuilding);
        }
    }
}
