using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Rules;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.PhaseLogic;
using System.Linq;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class CardDiscardingPhase : BasePhase
    {
        private ResourceCostOrStock _resourcesSelected = new();
        private Player _currentPlayer;

        public CardDiscardingPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter()
        {
            if (Game.CardDiscardingProgress == null)
            {
                var ids = Game.GetCardsDiscardingPlayers().Select(p => p.ID);

                Game.CreateCardDiscardingContext(ids);
            }
        }

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
            var context = Game.CardDiscardingProgress;


            if (context.PlayersToDiscard.Count == 0)
            {
                Game.CardsDiscardedContextClear();
                PhaseTransition.ChangePhase(EnumGamePhases.RobberPlacing);
                return;
            }

            _currentPlayer = Game.GetPlayerById(Game.CardDiscardingProgress.PlayersToDiscard.Peek());
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
            var result = DiscardCardsLogic.Handle(Game, _currentPlayer, _resourcesSelected);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_currentPlayer.ID, result.Reason));

                return;
            }

            ProceedToNextPlayer();
        }
    }
}