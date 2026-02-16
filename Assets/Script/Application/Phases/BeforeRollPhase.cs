using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using System.Linq;

namespace Catan.Application.Phases
{
    public class BeforeRollPhase : BasePhase
    {
        public BeforeRollPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

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
            var result = Facade.UseRollDice();

            var byPlayer = result.Distributions.GroupBy(d => new { d.PlayerId, d.PlayerName });

            foreach (var playerGroup in byPlayer)
            {
                var resources = playerGroup.GroupBy(d => d.Type).Select(g => $"{g.Sum(x => x.Granted)} {g.Key.ToString().ToLower()}").ToList();

                if (resources.Count == 0)
                    continue;

                var text = $"{playerGroup.Key.PlayerName}: {string.Join(", ", resources)}";

                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, text));
            }

            TransitionPhase(result);
        }

        private void HandleInvalidClick()
        {
            var playerId = Facade.GetCurrentPlayerId();

            Bus.Publish(new ActionRejectedEvent(playerId, ConditionFailureReason.NotRolledYet));
        }
    }
}