using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Application.CommandHandlers
{
    public sealed class StealCardHandler
    {
        private GameState _game;

        public StealCardHandler(GameState game)
        {
            _game = game;
        }

        public ResultResourceTheft Handle(int victimId, EnumResourceTypes resource)
        {
            var thiefId = _game.CurrentPlayer.ID;
            var thief = _game.GetPlayerById(thiefId);
            var victim = _game.GetPlayerById(victimId);

            if (!RulesCardTheft.CanSteal(victim))
            {
                return ResultResourceTheft.Fail(thiefId, victimId, ConditionFailureReason.NoResourceCardsLeft);
            }

            _game.CardStolenMutaton(victim, resource);

            return ResultResourceTheft.Ok(thiefId, victimId, resource);
        }
    }
}
