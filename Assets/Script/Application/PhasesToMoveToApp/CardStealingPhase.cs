using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Core.PhaseLogic;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class CardStealingPhase : BasePhase
    {
        public CardStealingPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    HandleSteal(c);
                    break;
            }
        }

        private void HandleSteal(StolenCardSelectedCommand signal)
        {
            var victimId = Game.CardStealingProgress.VictimId;
            var result = StealCardLogic.Handle(Game, victimId, signal.Type);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(victimId, result.Reason));
                return;
            }

            FinishPhase();
        }

        private void FinishPhase()
        {
            if (Game.AfterRoll)
            {
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }

            else
            {
                PhaseTransition.ChangePhase(EnumGamePhases.BeforeRoll);
            }
        }
    }
}