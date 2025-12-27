using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public class LogicBeforeRoll : BasePhaseLogic
    {
        public LogicBeforeRoll(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case RollDiceCommand c:
                    HandleRollDiceClicked(c);
                    break;

                case VertexClickedCommand c:
                    HandleVertexClicked(c);
                    break;

                case EdgeClickedCommand c:
                    HandleEdgeClicked(c);
                    break;

                case HexClickedCommand c:
                    HandleHexClicked(c);
                    break;

                case ShowDevelopmentCardsCommand c:
                    Bus.Publish(new ProceedToDevelopmentCardsEvent(Game.GetCurrentPlayerDevelopmentCardIds(), Game.GetAfterRoll()));
                    break;
            }
        }

        private void HandleRollDiceClicked(RollDiceCommand signal)
        {
            var resultDiceRoll = Game.RollAndServePlayers();
            Game.SetAfterRollTo(true);

            foreach (var r in resultDiceRoll.Distributions)
            {
                if (r.Granted == 0)
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Bank has no {r.Type} to give to {r.PlayerId}"));
                }

                if (r.Granted < r.Requested)
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Bank has not enough {r.Type}, {r.PlayerId} gets {r.Granted} {r.Type}"));
                }

                else
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"{r.PlayerId} received {r.Granted} {r.Type}"));   
                }
            }

            bool rolledSeven = resultDiceRoll.Roll == 7;
            Bus.Publish(new DiceRollCompletedEvent(rolledSeven));
        }

        private void HandleVertexClicked(VertexClickedCommand signal)
        {
            Bus.Publish(new LogMessageEvent(EnumLogTypes.Warning, "Roll first"));
        }

        private void HandleEdgeClicked(EdgeClickedCommand signal)
        {
            Bus.Publish(new LogMessageEvent(EnumLogTypes.Warning, "Roll first"));
        }

        private void HandleHexClicked(HexClickedCommand signal)
        {
            Bus.Publish(new LogMessageEvent(EnumLogTypes.Warning, "Roll first"));
        }
    }
}