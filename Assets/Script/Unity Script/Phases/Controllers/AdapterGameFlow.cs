using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;

namespace Catan.Unity.Phases.Controllers
{
    public sealed class AdapterGameFlow
    {
        private readonly AdapterPhaseTransition _phases;
        private readonly ManagerUI _ui;
        private readonly EventBus _bus;
        private readonly Facade _facade;

        public AdapterGameFlow(ManagerUI ui, EventBus bus, Facade facade, AdapterPhaseTransition phases)
        {
            _ui = ui;
            _bus = bus;
            _phases = phases;
            _facade = facade;

            bus.Subscribe<PhaseChangedEvent>(OnPhaseChanged);
        }

        private void OnPhaseChanged(PhaseChangedEvent signal)
        {
            switch (signal.Phase)
            {
                case EnumGamePhases.BankTrade:
                    _phases.TransitionTo(new AdapterBankTrade(_ui, _bus, _facade));
                    break;

                case EnumGamePhases.NormalRound:
                    _phases.TransitionTo(new AdapterNormalRound());
                    break;

                case EnumGamePhases.BeforeRoll:
                    _phases.TransitionTo(new AdapterBeforeRoll());
                    break;

                case EnumGamePhases.TradeOffer:
                    _phases.TransitionTo(new AdapterTradeOffer());
                    break;

                case EnumGamePhases.TradeRequest:
                    _phases.TransitionTo(new AdapterTradeRequest());
                    break;

                case EnumGamePhases.RobberPlacing:
                    _phases.TransitionTo(new AdapterRobberPlacing());
                    break;

                case EnumGamePhases.CardDiscarding:
                    _phases.TransitionTo(new AdapterCardDiscarding());
                    break;

                case EnumGamePhases.CardStealing:
                    _phases.TransitionTo(new AdapterCardStealing());
                    break;

                case EnumGamePhases.DevelopmentCards:
                    _phases.TransitionTo(new AdapterDevelopmentCards());
                    break;

                case EnumGamePhases.MonopolyCard:
                    _phases.TransitionTo(new AdapterMonopolyCard());
                    break;

                case EnumGamePhases.YearOfPlentyCard:
                    _phases.TransitionTo(new AdapterYearOfPlentyCard());
                    break;

                case EnumGamePhases.RoadBuilding:
                    _phases.TransitionTo(new AdapterRoadBuilding());
                    break;

                case EnumGamePhases.FirstRoundsBuilding:
                    _phases.TransitionTo(new AdapterFirstRoundsBuilding());
                    break;
            }
        }
    }
}