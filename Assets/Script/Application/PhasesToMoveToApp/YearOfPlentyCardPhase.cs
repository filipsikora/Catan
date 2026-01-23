using Catan.Application.Controllers;
using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.PhaseLogic;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class YearOfPlentyCardPhase : BasePhase
    {
        private ResourceCostOrStock _cardsDesired = new();
        private readonly int _cardsToReceive = 2;

        public YearOfPlentyCardPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceCardClicked(c);
                    break;

                case CardSelectionAcceptedCommand c:
                    HandleResourcesSelected(c);
                    break;
            }
        }

        private void HandleResourceCardClicked(ResourceCardSelectedCommand signal)
        {
            EnumResourceTypes type = signal.Type;
            var availability = ConditionsTrade.BankHasEnoughResources(Game.Bank, type);

            if (!signal.IsSelected)
            {
                if (_cardsDesired.Get(type) > 0)
                {
                    _cardsDesired.SubtractExactAmount(type, 1);
                }
            }

            if (signal.IsSelected)
            {
                if (availability.Success)
                {
                    _cardsDesired.AddExactAmount(type, 1);
                }

                else
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Error, "Not enough resources in the bank"));
                }
            }

            bool canAccept = ConditionsResources.HasExactResourcesNumber(_cardsDesired, 2).Success;

            Bus.Publish(new SelectionChangedEvent(canAccept));
        }

        private void HandleResourcesSelected(CardSelectionAcceptedCommand signal)
        {
            var player = Game.GetCurrentPlayer();
            var result = UseYearOfPlentyLogic.Handle(Game, _cardsDesired);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));
            }

            foreach (var (key, amount) in result.Requested.ResourceDictionary)
            {
                if (amount <= 0)
                    continue;

                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Player{player.ID} received {key} {amount} from Year Of Plenty card"));
            }

            PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
        }
    }
}