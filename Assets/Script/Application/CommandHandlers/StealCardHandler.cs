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

        public ResultStealResource Handle(int victimId, EnumResourceTypes resource)
        {
            var thiefId = _game.CurrentPlayer.ID;
            var thief = _game.GetPlayerById(thiefId);
            var victim = _game.GetPlayerById(victimId);

            var result = RulesRobber.CanSteal(victim);

            if (!result.Success)
            {
                return ResultStealResource.Fail(thiefId, victimId, result.Reason);
            }

            _game.CardStolenMutation(victim, resource);

            return ResultStealResource.Ok(thiefId, victimId, resource);
        }
    }
}