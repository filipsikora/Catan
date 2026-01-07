using Catan.Core.Engine;
using Catan.Core.Results;

namespace Catan.Application.CommandHandlers
{
    public class RollDiceHandler
    {
        private readonly GameState _game;

        public RollDiceHandler(GameState game)
        {
            _game = game;
        }

        public ResultDiceRoll Handle()
        {
            var result = _game.RollAndServePlayers();
            _game.SetAfterRollTo(true);

            return result;
        }
    }
}