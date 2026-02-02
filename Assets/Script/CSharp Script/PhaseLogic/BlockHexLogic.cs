using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BlockHexLogic : BaseLogic
    {
        public BlockHexLogic(GameSession session) : base(session) { }

        public ResultBlockHex Handle(int hexId)
        {
            var hex = Session.GetHexById(hexId);
            var result = RulesRobber.CanBlock(hex);

            if (!result.Success)
            {
                return ResultBlockHex.Fail(result.Reason);
            }

            Session.BlockHexMutation(hex);

            return ResultBlockHex.Ok(hex.Id);
        }
    }
}