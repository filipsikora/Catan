using Catan.Shared.Communication;
using Catan.Core.Engine;
using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Application.Controllers;
using Catan.Core.Models;
using Catan.Shared.Data;
using Catan.Core.PhaseLogic;
using System.Linq;
using System.Collections.Generic;

namespace Catan.Application.Phases
{
    public class RobberPlacingPhase : BasePhase
    {
        private bool clickableHexes = true;

        public RobberPlacingPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

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
            var result = BlockHexLogic.Handle(Game, hexId);
            var hex = Game.Map.GetHexById(hexId);
            var thief = Game.GetCurrentPlayer();

            if (!result.Success)
                return;

            Bus.Publish(new RobberPlacedEvent(hex.Id));

            HandleVictimsAfterBlocking(hex, thief);

            clickableHexes = false;
        }

        private void HandleVictimsAfterBlocking(HexTile hex, Player thief)
        {
            var victims = Game.GetPlayersAdjacentToHex(hex);

            victims.Remove(thief);

            List<int> victimsIds = victims.Select(v => v.ID).ToList();

            if (victims.Count == 0)
            {
                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Noone to steal from"));

                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }

            else
            {
                Bus.Publish(new PotentialVictimsFoundEvent(victimsIds));
            }
        }

        private void VictimChosen(VictimChosenCommand signal)
        {
            Game.CreateCardsStealingContext(signal.VictimId);

            PhaseTransition.ChangePhase(EnumGamePhases.CardStealing);
        }
    }
}