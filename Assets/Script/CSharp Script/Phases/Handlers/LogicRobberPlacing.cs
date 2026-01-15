using Catan.Shared.Communication;
using Catan.Core.Engine;
using System.Linq;
using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Application.CommandHandlers;
using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public class LogicRobberPlacing : BasePhaseLogic
    {
        private bool clickableHexes = true;
        private BlockHexHandler _handler;

        public LogicRobberPlacing(GameState game, EventBus bus) : base(game, bus)
        {
            _handler = new BlockHexHandler(game);
        }

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

            var hexId = signal.HexId;
            var result = _handler.Handle(hexId);
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

            if (victims.Count == 0)
            {
                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Noone to steal from"));
                Bus.Publish(new CardStealingCompletedEvent());
            }

            else
            {
                Bus.Publish(new PotentialVictimsFoundEvent());
            }
        }
    }
}