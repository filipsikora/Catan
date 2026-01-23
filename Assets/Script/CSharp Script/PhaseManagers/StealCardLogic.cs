using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public static class StealCardLogic
    {
        public static ResultStealResource Handle(GameState game, int victimId, EnumResourceTypes resource)
        {
            var thiefId = game.CurrentPlayer.ID;
            var thief = game.GetPlayerById(thiefId);
            var victim = game.GetPlayerById(victimId);

            var result = RulesRobber.CanSteal(victim, game.CardStealingProgress);

            if (!result.Success)
            {
                return ResultStealResource.Fail(thiefId, victimId, result.Reason);
            }

            game.CardStolenMutation(victim, resource);
            game.CardStealingContextClear();

            return ResultStealResource.Ok(thiefId, victimId, resource);
        }
    }
}