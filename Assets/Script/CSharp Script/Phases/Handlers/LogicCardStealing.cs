using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;

namespace Catan.Core.Phases.Handlers
{
    public class LogicCardStealing : BasePhaseLogic
    {
        private int _victimId;
        private int _thiefId;

        private StealCardHandler _handler;

        public LogicCardStealing(GameState game, EventBus bus, int victimId) : base(game, bus)
        {
            _victimId = victimId;
            _handler = new StealCardHandler(game);
        }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    HandleSteal(c);
                    break;

                case RequestCardStealingStartCommand c:
                    HandleCardStealingStarted(c);
                    break;
            }
        }

        private void HandleCardStealingStarted(RequestCardStealingStartCommand signal)
        {
            var player = Game.GetCurrentPlayer();
            var victim = Game.GetPlayerById(_victimId);

            _thiefId = player.ID;

            if (!Rules.RulesCardTheft.CanSteal(victim))
            {
                Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Info, "Nothing to steal from this player"));

                FinishPhase();

                return;
            }

            else
            {
                Bus.Publish(new VictimSelectedEvent(victim.Resources, victim.ID));
            }
        }

        private void HandleSteal(StolenCardSelectedCommand signal)
        {
            var result = _handler.Handle(_victimId, signal.Type);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_victimId, result.Reason.Value));
            }

            Bus.Publish(new PlayerStateChangedEvent(_thiefId));
            Bus.Publish(new PlayerStateChangedEvent(_victimId));

            FinishPhase();
        }

        private void FinishPhase()
        {
            Bus.Publish(new CardStealingCompletedEvent());
        }
    }
}