using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using System.Linq;

namespace Catan.Core.Phases.Handlers
{
    public class LogicCardStealing : BasePhaseLogic
    {
        private int _victimId;
        private Player _victim;

        public LogicCardStealing(GameState game, EventBus bus, int victimId) : base(game, bus)
        {
            _victimId = victimId;
        }

        public override void Enter()
        {
            _victim = Game.GetPlayerById(_victimId);
        }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    HandleResourceSelectionChanged(c);
                    break;

                case RequestCardStealingStartCommand c:
                    HandleCardStealingStarted(c);
                    break;
            }
        }

        private void HandleCardStealingStarted(RequestCardStealingStartCommand signal)
        {
            if (_victim.Resources.ResourceDictionary.Values.Sum() == 0)
            {
                Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Info, "Nothing to steal from this player"));

                FinishPhase();
            }

            else
            {
                Bus.Publish(new VictimSelectedEvent(_victim.Resources, _victim.ID));
            }
        }

        private void HandleResourceSelectionChanged(StolenCardSelectedCommand signal)
        {
            Game.CurrentPlayer.Resources.AddExactAmount(signal.Type, 1);
            _victim.Resources.SubtractExactAmount(signal.Type, 1);

            FinishPhase();
        }

        private void FinishPhase()
        {
            Bus.Publish(new CardStealingCompletedEvent());
        }
    }
}