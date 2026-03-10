using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Rules;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using System.Collections.Generic;

namespace Catan.Core.Phases.Handlers
{
    public class LogicCardDiscarding : BasePhaseLogic
    {
        private DiscardCardsHandler _handler;

        private Queue<Player> _playersToDiscard;
        private ResourceCostOrStock _resourcesSelected = new();
        private Player _currentPlayer;

        public LogicCardDiscarding(GameState game, EventBus bus) : base(game, bus)
        {
            _handler = new DiscardCardsHandler(game);
        }

        public override void Enter()
        {
            _playersToDiscard = Game.GetCardsDiscardingPlayers();
        }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceSelectionChanged(c);
                    break;

                case DiscardingAcceptedCommand c:
                    HandleDiscardingAccepted(c);
                    break;

                case RequestCardDiscardingStartCommand c:
                    ProceedToNextPlayer();
                    break;
            }
        }

        private void ProceedToNextPlayer()
        {
            if (_playersToDiscard.Count == 0)
            {
                Bus.Publish(new ProceedToRobberPlacingEvent());
                return;
            }

            _currentPlayer = _playersToDiscard.Dequeue();
            _resourcesSelected = new ResourceCostOrStock();

            Bus.Publish(new PlayerSelectedToDiscardEvent(_currentPlayer.ID));
        }

        private void HandleResourceSelectionChanged(ResourceCardSelectedCommand signal)
        {
            if (!signal.IsSelected)
            {
                _resourcesSelected.AddExactAmount(signal.Type, 1);
            }

            else
            {
                _resourcesSelected.SubtractExactAmount(signal.Type, 1);
            }

            bool canDiscard = RulesCardDiscard.CanDiscard(_currentPlayer, _resourcesSelected).Success;

            Bus.Publish(new SelectionChangedEvent(canDiscard));
        }

        private void HandleDiscardingAccepted(DiscardingAcceptedCommand signal)
        {
            var result = _handler.Handle(_currentPlayer, _resourcesSelected);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_currentPlayer.ID, result.Reason));

                return;
            }

            ProceedToNextPlayer();
        }
    }
}