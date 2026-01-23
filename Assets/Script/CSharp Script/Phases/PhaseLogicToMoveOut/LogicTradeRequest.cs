using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Application.CommandHandlers;

namespace Catan.Core.Phases.Handlers
{
    public class LogicTradeRequest : BasePhaseLogic
    {
        private int _buyerId;
        private ResourceCostOrStock _cardsDesired;
        private ResourceCostOrStock _cardsOffered;

        private ReactToTradeLogic _handler;

        public LogicTradeRequest(GameState game, EventBus bus, int playerId, ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsDesired) : base(game, bus)
        {
            _buyerId = playerId;
            _cardsOffered = cardsOffered;
            _cardsDesired = cardsDesired;
            _handler = new ReactToTradeLogic(game);
        }

        public override void Enter() { }

        public override void Exit() { }

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
            var buyer = Game.GetPlayerById(_buyerId);

            var result = _handler.Handle(seller.ID, _buyerId, _cardsOffered, _cardsDesired, Game.LastPlayerTradeOffered);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_buyerId, result.Reason));

                return;
            }

            Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Info, "Trade accepted"));

            HandleTradeFinished();
        }

        private void HandleTradeFinished()
        {
            // set context to null //

            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}