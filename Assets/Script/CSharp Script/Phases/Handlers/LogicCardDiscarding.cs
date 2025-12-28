using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catan.Core.Phases.Handlers
{
    public class LogicCardDiscarding : BasePhaseLogic
    {
        private Queue<Player> _playersToDiscard;
        private ResourceCostOrStock _resourcesSelected = new();
        private Player _currentPlayer;
        private int _requiredResources;

        public LogicCardDiscarding(GameState game, EventBus bus) : base(game, bus) { }

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
            UpdateRequiredResources();
        }

        private bool UpdateRequiredResources()
        {
            int total = _currentPlayer.Resources.ResourceDictionary.Values.Sum();
            _requiredResources = (int)System.Math.Ceiling(total / 2.0);
            bool canDiscard = _resourcesSelected.ResourceDictionary.Values.Sum() == _requiredResources;

            return canDiscard;
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

            bool canDiscard = UpdateRequiredResources();

            Bus.Publish(new SelectionChangedEvent(canDiscard));
        }

        private void HandleDiscardingAccepted(DiscardingAcceptedCommand signal)
        {
            _currentPlayer.Resources.SubtractExact(_resourcesSelected);
            Game.Bank.AddExact(_resourcesSelected);

            ProceedToNextPlayer();
        }
    }
}