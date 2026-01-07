using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public class LogicBeforeRoll : BasePhaseLogic
    {
        private RollDiceHandler _handler;

        public LogicBeforeRoll(GameState game, EventBus bus) : base(game, bus)
        {
            _handler = new RollDiceHandler(game);
        }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case RollDiceCommand c:
                    HandleRollDice(c);
                    break;

                case ShowDevelopmentCardsCommand c:
                    Bus.Publish(new ProceedToDevelopmentCardsEvent());
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
            var result = _handler.Handle();

            foreach (var r in result.Distributions)
            {
                if (r.Granted == 0)
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Bank has no {r.Type} to give to Player{r.PlayerId}"));
                }

                if (r.Granted < r.Requested)
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Bank has not enough {r.Type}, Player{r.PlayerId} gets {r.Granted} {r.Type}"));
                }

                else
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Player{r.PlayerId} received {r.Granted} {r.Type}"));   
                }
            }

            bool rolledSeven = result.Roll == 7;
            Bus.Publish(new DiceRollCompletedEvent(rolledSeven));
        }

        private void HandleInvalidClick()
        {
            var player = Game.GetCurrentPlayer();

            Bus.Publish(new ActionRejectedEvent(player.ID, ConditionFailureReason.NotRolledYet));
        }
    }
}