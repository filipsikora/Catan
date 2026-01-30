using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BlockHexLogic
    {
        private readonly GameSession _session;

        public BlockHexLogic(GameSession session)
        {
            _session = session;
        }

        public ResultBlockHex Handle(int hexId)
        {
            var hex = _session.GetHexById(hexId);
            var result = RulesRobber.CanBlock(hex);

            if (!result.Success)
            {
                return ResultBlockHex.Fail(result.Reason);
            }

            _session.BlockHexMutation(hex);

            return ResultBlockHex.Ok(hex.Id);
        }
    }
}