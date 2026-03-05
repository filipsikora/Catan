using Catan.Application.Controllers;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Controllers
{
    public sealed class AdapterGameFlow
    {
        private readonly AdapterPhaseTransition _phases;
        private readonly ManagerUI _ui;
        private readonly EventBus _bus;
        private readonly Facade _facade;
        private readonly VisualsBoard _board;
        private readonly HandlerEvents _eventsHandler;

        public AdapterGameFlow(ManagerUI ui, EventBus bus, Facade facade, AdapterPhaseTransition phases, VisualsBoard board, HandlerEvents eventsHandler)
        {
            _ui = ui;
            _bus = bus;
            _phases = phases;
            _facade = facade;
            _board = board;
            _eventsHandler = eventsHandler;
        }

        public void ChangePhase(EnumGamePhases nextPhase)
        {
            switch (nextPhase)
            {
                case EnumGamePhases.BankTrade:
                    _phases.TransitionTo(new AdapterBankTrade(_ui, _bus, _facade, _eventsHandler));
                    break;

                case EnumGamePhases.NormalRound:
                    _phases.TransitionTo(new AdapterNormalRound(_ui, _bus, _facade, _board));
                    break;

                case EnumGamePhases.BeforeRoll:
                    _phases.TransitionTo(new AdapterBeforeRoll(_ui, _bus, _facade, _eventsHandler));
                    break;

                case EnumGamePhases.TradeOffer:
                    _phases.TransitionTo(new AdapterTradeOffer(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.TradeRequest:
                    _phases.TransitionTo(new AdapterTradeRequest(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.RobberPlacing:
                    _phases.TransitionTo(new AdapterRobberPlacing(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.CardDiscarding:
                    _phases.TransitionTo(new AdapterCardDiscarding(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.CardStealing:
                    _phases.TransitionTo(new AdapterCardStealing(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.DevelopmentCards:
                    _phases.TransitionTo(new AdapterDevelopmentCards(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.MonopolyCard:
                    _phases.TransitionTo(new AdapterMonopolyCard(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.YearOfPlentyCard:
                    _phases.TransitionTo(new AdapterYearOfPlentyCard(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.RoadBuilding:
                    _phases.TransitionTo(new AdapterRoadBuilding(_ui, _bus, _facade, _board));
                    break;

                case EnumGamePhases.FirstRoundsBuilding:
                    _phases.TransitionTo(new AdapterFirstRoundsBuilding(_ui, _bus, _facade, _board));
                    break;
            }
        }
    }
}