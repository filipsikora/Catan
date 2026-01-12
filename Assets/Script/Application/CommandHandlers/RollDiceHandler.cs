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

        public ResultRollDice Handle()
        {
            _game.RollDice();

            var resultDistributionList = _game.ServePlayersMutation();
            var resultRoll = _game.DiceRolledMutation();
            var result = new ResultRollDice(resultRoll, resultDistributionList);

            return result;
        }
    }
}