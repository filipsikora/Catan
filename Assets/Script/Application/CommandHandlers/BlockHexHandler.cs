using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BlockHexHandler
    {
        private GameState _game;

        public BlockHexHandler(GameState game)
        {
            _game = game;
        }

        public ResultBlockHex Handle(int hexId)
        {
            var hex = _game.Map.GetHexById(hexId);
            var result = RulesRobber.CanBlock(hex);

            if (!result.Success)
            {
                return ResultBlockHex.Fail(result.Reason);
            }

            _game.BlockHexMutation(hex);

            return ResultBlockHex.Ok(hex.Id);
        }
    }
}
