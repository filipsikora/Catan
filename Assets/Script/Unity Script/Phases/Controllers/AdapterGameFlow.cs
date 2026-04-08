using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Networking;
using System;

namespace Catan.Unity.Phases.Controllers
{
    public sealed class AdapterGameFlow
    {
        private readonly AdapterPhaseTransition _phases;
        private readonly ManagerUI _ui;
        private readonly EventBus _bus;
        private HandlerEvents _eventsHandler;
        private GameClient _client;
        private Guid _gameId;

        public AdapterGameFlow(ManagerUI ui, EventBus bus, AdapterPhaseTransition phases, GameClient client, Guid gameId)
        {
            _ui = ui;
            _bus = bus;
            _phases = phases;
            _client = client;
            _gameId = gameId;
        }

        public void Initialize(HandlerEvents eventsHandler)
        {
            _eventsHandler = eventsHandler;
        }

        public void ChangePhase(EnumGamePhases nextPhase)
        {
            switch (nextPhase)
            {
                case EnumGamePhases.BankTrade:
                    _phases.TransitionTo(new AdapterBankTrade(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.NormalRound:
                    _phases.TransitionTo(new AdapterNormalRound(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.BeforeRoll:
                    _phases.TransitionTo(new AdapterBeforeRoll(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.TradeOffer:
                    _phases.TransitionTo(new AdapterTradeOffer(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.TradeRequest:
                    _phases.TransitionTo(new AdapterTradeRequest(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.RobberPlacing:
                    _phases.TransitionTo(new AdapterRobberPlacing(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.CardDiscarding:
                    _phases.TransitionTo(new AdapterCardDiscarding(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.CardStealing:
                    _phases.TransitionTo(new AdapterCardStealing(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.DevelopmentCards:
                    _phases.TransitionTo(new AdapterDevelopmentCards(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.MonopolyCard:
                    _phases.TransitionTo(new AdapterMonopolyCard(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.YearOfPlentyCard:
                    _phases.TransitionTo(new AdapterYearOfPlentyCard(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.RoadBuilding:
                    _phases.TransitionTo(new AdapterRoadBuilding(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;

                case EnumGamePhases.FirstRoundsBuilding:
                    _phases.TransitionTo(new AdapterFirstRoundsBuilding(_ui, _bus, _eventsHandler, _client, _gameId));
                    break;
            }
        }
    }
}