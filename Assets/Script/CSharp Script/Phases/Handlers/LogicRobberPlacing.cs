using Catan.Shared.Communication;
using Catan.Core.Engine;
using Catan.Core.Models;
using System.Linq;
using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;

namespace Catan.Core.Phases.Handlers
{
    public class LogicRobberPlacing : BasePhaseLogic
    {
        private bool clickableHexes = true;

        public LogicRobberPlacing(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter()
        {
            Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Info, "Choose a hex to block"));
        }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case HexClickedCommand c:
                    HandleHexClicked(c);
                    break;

                case VictimChosenCommand c:
                    Bus.Publish(new RobberPlacingToCardStealingEvent(c.VictimId));
                    break;
            }
        }

        private void HandleHexClicked(HexClickedCommand signal)
        {
            if (!clickableHexes)
                return;

            var _hexId = signal.HexId;
            HexTile hex = Game.Map.GetHexById(_hexId);

            if (hex.isBlocked)
            {
                Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Error, "You have to move the robber."));
                return;
            }

            Game.BlockHex(hex);

            var victims = Game.GetPlayersAdjacentToHex(hex);
            victims.Remove(Game.GetCurrentPlayer());

            var victimsIds = victims.Select(v => v.ID).ToList();

            clickableHexes = false;

            Bus.Publish(new RobberPlacedEvent(hex.Id));

            if (victimsIds.Count == 0)
            {
                Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Info, "Noone to steal from"));

                var afterRoll = Game.GetAfterRoll();

                Bus.Publish(new CardStealingCompletedEvent());
            }

            else
            {
                Bus.Publish(new PotentialVictimsSelectedEvent(victimsIds));
            }
        }
    }
}