using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class DevelopmentCardsPhase : BasePhase
    {
        public DevelopmentCardsPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case DevelopmentCardClickedCommand c:
                    HandlePlayDevCard(c);
                    break;

                case DevelopmentCardsCanceledCommand c:
                    FinishPhase();
                    break;
            }
        }

        private void HandlePlayDevCard(DevelopmentCardClickedCommand signal)
        {
            var result = Facade.UseDevCard(signal.DevelopmentCardId);
            var playerId = Facade.GetCurrentPlayerId();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(playerId, result.Reason));

                FinishPhase();

                return;
            }

            // change phase to result //
        }

        public void FinishPhase()
        {
            bool afterRoll = Facade.GetAfterRoll();

            if (afterRoll)
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