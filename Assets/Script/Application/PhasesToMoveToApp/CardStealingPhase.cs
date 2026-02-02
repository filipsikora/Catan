using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class CardStealingPhase : BasePhase
    {
        public CardStealingPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

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
            var victimId = Facade.GetVictimId();
            var result = Facade.UseSteal(victimId, signal.Type);

            if (!result.Success) 
            {
                Bus.Publish(new ActionRejectedEvent(victimId, result.Reason));
                return;
            }

            FinishPhase();
        }

        private void FinishPhase()
        {
            if (Facade.GetAfterRoll())
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