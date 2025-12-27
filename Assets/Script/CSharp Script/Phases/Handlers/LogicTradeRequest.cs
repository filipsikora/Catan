using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Helpers;
using Catan.Shared.Results;

namespace Catan.Core.Phases.Handlers
{
    public class LogicTradeRequest : BasePhaseLogic
    {
        private int _playerOfferedId;
        private Player _playerOffered;
        private ResourceCostOrStock _cardsDesired;
        private ResourceCostOrStock _cardsOffered;

        public LogicTradeRequest(GameState game, EventBus bus, int playerId, ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsDesired) : base(game, bus)
        {
            _playerOfferedId = playerId;
            _cardsOffered = cardsOffered;
            _cardsDesired = cardsDesired;
        }

        public override void Enter()
        {
            _playerOffered = Game.GetPlayerById(_playerOfferedId);
        }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case TradeRequestAcceptedCommand c:
                    HandleTradeRequestAccepted(c);
                    break;

                case TradeRequestRefusedCommand c:
                    FinishTrade();
                    break;

                case RequestTradeRequestValidatedCommand c:
                    HandleTradeRequestValidationRequested(c);
                    break;
            }
        }

        private void HandleTradeRequestValidationRequested(RequestTradeRequestValidatedCommand signal)
        {
            ResultCondition result = Conditions.CanAfford(_playerOffered.Resources, _cardsDesired);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_playerOfferedId, result.Reason));
            }

            Bus.Publish(new TradeRequestSentEvent(_playerOfferedId, result.Success, _cardsOffered, _cardsDesired));
        }

        private void HandleTradeRequestAccepted(TradeRequestAcceptedCommand signal)
        {
            var playerOffering = Game.GetCurrentPlayer();
            playerOffering.Resources.AddExact(_cardsDesired);
            _playerOffered.Resources.SubtractExact(_cardsDesired);

            playerOffering.Resources.SubtractExact(_cardsOffered);
            _playerOffered.Resources.AddExact(_cardsOffered);

            FinishTrade();
        }

        private void FinishTrade()
        {
            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}
