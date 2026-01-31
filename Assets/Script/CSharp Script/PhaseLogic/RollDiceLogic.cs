using Catan.Core.Results;

namespace Catan.Core.PhaseLogic
{
    public sealed class RollDiceLogic : BaseLogic
    {
        public RollDiceLogic(GameSession session) : base(session) { }

        public ResultRollDice Handle()
        {
            Session.RollDice();

            var resultDistributionList = Session.ServePlayersMutation();
            var resultRoll = Session.DiceRolledMutation();
            var result = new ResultRollDice(resultRoll, resultDistributionList);

            if (resultRoll == 7)
                Session.GetPlayersToDiscard();

            return result;
        }
    }
}