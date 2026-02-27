using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
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

        public AdapterGameFlow(ManagerUI ui, EventBus bus, Facade facade, AdapterPhaseTransition phases, VisualsBoard board)
        {
            _ui = ui;
            _bus = bus;
            _phases = phases;
            _facade = facade;
            _board = board;

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
                    _phases.TransitionTo(new AdapterNormalRound(_ui, _bus, _facade, _board));
                    break;

                case EnumGamePhases.BeforeRoll:
                    _phases.TransitionTo(new AdapterBeforeRoll(_ui, _bus, _facade));
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
                    UnityEngine.Debug.Log("chuj adapter");
                    break;
            }
        }
    }
}