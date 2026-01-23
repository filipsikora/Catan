using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Application.Controllers;
using Catan.Core.PhaseLogic;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class TradeRequestPhase : BasePhase
    {
        private PlayerTradeContext _context;

        public TradeRequestPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter()
        {
            _context = Game.LastPlayerTradeOffered;
        }

        public override void Handle(object command)
        {
            switch (command)
            {
                case RefuseTradeRequestCommand c:
                    HandleTradeFinished();
                    break;

                case AcceptTradeRequestCommand c:
                    HandleTradeAccepted(c);
                    break;
            }
        }

        private void HandleTradeAccepted(AcceptTradeRequestCommand signal)
        {
            var seller = Game.GetCurrentPlayer();
            var buyer = Game.GetPlayerById(_context.BuyerId);

            var result = ReactToTradeLogic.Handle(Game);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_context.BuyerId, result.Reason));

                return;
            }

            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Trade accepted"));

            HandleTradeFinished();
        }

        private void HandleTradeFinished()
        {
            Game.PlayerTradeOfferedContextClear();

            PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
        }
    }
}