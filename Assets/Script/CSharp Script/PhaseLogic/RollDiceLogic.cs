using Catan.Core.Engine;
using Catan.Core.Results;

namespace Catan.Core.PhaseLogic.Logic
{
    public static class RollDiceLogic
    {
        public static ResultRollDice Handle(GameState game)
        {
            game.RollDice();

            var resultDistributionList = game.ServePlayersMutation();
            var resultRoll = game.DiceRolledMutation();
            var result = new ResultRollDice(resultRoll, resultDistributionList);

            return result;
        }
    }
}