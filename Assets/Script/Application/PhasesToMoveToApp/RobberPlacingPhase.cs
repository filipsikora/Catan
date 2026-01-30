using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Application.Controllers;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Phases
{
    public class RobberPlacingPhase : BasePhase
    {
        private bool clickableHexes = true;
        private List<int> _possibleVictimsIds = new();

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
            var result = Facade.BlockHex(hexId);
            var thiefId = Facade.GetCurrentPlayerId();

            if (!result.Success)
                return;

            Bus.Publish(new RobberPlacedEvent(hexId));

            HandleVictimsAfterBlocking(hexId, thiefId);

            clickableHexes = false;
        }

        private void HandleVictimsAfterBlocking(int hexId, int thiefId)
        {
            var possibleVictimsIds = Facade.GetAdjacentToHexPlayersIds(hexId);

            possibleVictimsIds.Remove(thiefId);

            _possibleVictimsIds = possibleVictimsIds;

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
            var result = Facade.SelectVictim(signal.VictimId, _possibleVictimsIds);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Facade.GetCurrentPlayerId(), result.Reason));

                return;
            }

            PhaseTransition.ChangePhase(EnumGamePhases.CardStealing);
        }
    }
}