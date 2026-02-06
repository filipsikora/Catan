using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class DiscardCardsLogic : BaseLogic
    {
        public DiscardCardsLogic(GameSession session) : base(session) { }

        public ResultCondition Handle(int playerId, ResourceCostOrStock selectedCards)
        {
            var player = Session.GetPlayerById(playerId);
            var result = RulesCardDiscard.CanDiscard(player, selectedCards);

            if (!result.Success)
            {
                return result;
            }

            Session.CardsDiscardedMutation(player, selectedCards);
            Session.CardsDiscardedContextMutation();

            var nextPhase = Session.GetPlayersToDiscardCount() == 0 ? EnumGamePhases.RobberPlacing : EnumGamePhases.None;

            return ResultCondition.Ok(nextPhase);
        }
    }
}