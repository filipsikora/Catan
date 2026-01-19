using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class BlockHexLogic
    {
        public static ResultBlockHex Handle(GameState game, int hexId)
        {
            var hex = game.Map.GetHexById(hexId);
            var result = RulesRobber.CanBlock(hex);

            if (!result.Success)
            {
                return ResultBlockHex.Fail(result.Reason);
            }

            game.BlockHexMutation(hex);

            return ResultBlockHex.Ok(hex.Id);
        }
    }
}
