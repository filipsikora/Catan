using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Application.Controllers;
using Catan.Shared.Data;
using Catan.Core.Results;

namespace Catan.Application.Phases
{
    public class RobberPlacingPhase : BasePhase
    {
        private bool clickableHexes = true;

        public RobberPlacingPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter()
        {
            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Choose a hex to block"));
        }

        public override void Handle(object command)
        {
            switch (command)
            {
                case HexClickedCommand c:
                    HandleHexClicked(c);
                    break;

                case VictimChosenCommand c:
                    VictimChosen(c);
                    break;
            }
        }

        private void HandleHexClicked(HexClickedCommand signal)
        {
            if (!clickableHexes)
                return;

            var hexId = signal.HexId;
            var result = Facade.UseBlockHex(hexId);

            if (!result.Success)
                return;

            Bus.Publish(new RobberPlacedEvent(hexId));

            HandleVictimsAfterBlocking();

            clickableHexes = false;
        }

        private void HandleVictimsAfterBlocking()
        {
            var possibleVictimsIds = Facade.GetPossibleVictimsIds();

            if (possibleVictimsIds.Count == 0)
            {
                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Noone to steal from"));
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }

            else
            {
                Bus.Publish(new PotentialVictimsFoundEvent(possibleVictimsIds));
            }
        }

        private void VictimChosen(VictimChosenCommand signal)
        {
            var result = Facade.UseSelectVictim(signal.VictimId);
            
            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Facade.GetCurrentPlayerId(), result.Reason));

                return;
            }

            PhaseTransition.ChangePhase(EnumGamePhases.CardStealing);
        }
    }
}