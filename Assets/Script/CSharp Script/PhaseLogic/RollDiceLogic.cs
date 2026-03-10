using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class RollDiceLogic : BaseLogic
    {
        public RollDiceLogic(GameSession session) : base(session) { }

        public ResultRollDice Handle()
        {
            Session.RollDice();

            var rolledSevenButNoVictims = false;
            var resultDistributionList = Session.ServePlayersMutation();
            var resultRoll = Session.DiceRolledMutation();
            var nextPhase = EnumGamePhases.NormalRound;

            if (resultRoll == 7)
            {
                if (Session.GetPlayersToDiscardCount() == 0)
                {
                    nextPhase = EnumGamePhases.NormalRound;
                    rolledSevenButNoVictims = true;
                }
                else
                {
                    Session.GetPlayersToDiscard();
                    nextPhase = EnumGamePhases.CardDiscarding;
                }
            }

            return ResultRollDice.Ok(resultRoll, resultDistributionList, nextPhase, rolledSevenButNoVictims); ;
        }
    }
}