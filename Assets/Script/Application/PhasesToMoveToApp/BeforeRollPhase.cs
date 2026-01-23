using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.PhaseLogic.Logic;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class BeforeRollPhase : BasePhase
    {
        public BeforeRollPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case RollDiceCommand c:
                    HandleRollDice(c);
                    break;

                case ShowDevelopmentCardsCommand c:
                    PhaseTransition.ChangePhase(EnumGamePhases.DevelopmentCards);
                    break;

                case VertexClickedCommand:
                case EdgeClickedCommand:
                case HexClickedCommand:
                    HandleInvalidClick();
                    break;
            }
        }

        private void HandleRollDice(RollDiceCommand signal)
        {
            var result = RollDiceLogic.Handle(Game);
            var text = new string("");

            foreach (var player in Game.PlayerList)
            {
                var resourcesReceived = new ResourceCostOrStock();

                foreach (var distribution in result.Distributions)
                {
                    if (distribution.PlayerId == player.ID)
                    {
                        resourcesReceived.AddExactAmount(distribution.Type, distribution.Granted);
                    }
                }

                text += $"{player.ID}:" + resourcesReceived.ToString() + "\n";
            }

            bool rolledSeven = result.Roll == 7;
            
            if (rolledSeven)
            {
                PhaseTransition.ChangePhase(EnumGamePhases.CardDiscarding);
            }

            else
            {
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }
        }

        private void HandleInvalidClick()
        {
            var player = Game.GetCurrentPlayer();

            Bus.Publish(new ActionRejectedEvent(player.ID, ConditionFailureReason.NotRolledYet));
        }
    }
}