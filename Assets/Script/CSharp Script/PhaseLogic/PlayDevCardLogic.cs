using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class PlayDevCardLogic : BaseLogic
    {
        public PlayDevCardLogic(GameSession session) : base(session) { }

        public ResultPlayDevCard Handle(int cardId)
        {
            var player = Session.GetCurrentPlayer();
            var card = Session.GetDevCardById(cardId);
            var afterRoll = Session.GetAfterRoll();
            var result = RulesDevCards.CanPlayDevCard(player, card, afterRoll);

            if (!result.Success)
            {
                return ResultPlayDevCard.Fail(result.Reason, player.ID);
            }

            if (card.Type == EnumDevelopmentCardTypes.YearOfPlenty)
            {
                result = RulesDevCards.CanPlayYearOfPlenty(Session.GetBank(), 2);
            }

            if (card.Type == EnumDevelopmentCardTypes.RoadBuilding)
            {
                result = RulesDevCards.CanPlayRoadBuilding(player);
            }

            if (!result.Success)
            {
                return ResultPlayDevCard.Fail(result.Reason, player.ID);
            }

            EnumGamePhases nextPhase = card.Type switch
            {
                EnumDevelopmentCardTypes.Knight => EnumGamePhases.RobberPlacing,
                EnumDevelopmentCardTypes.Monopoly => EnumGamePhases.MonopolyCard,
                EnumDevelopmentCardTypes.RoadBuilding => EnumGamePhases.RoadBuilding,
                EnumDevelopmentCardTypes.VictoryPoint => EnumGamePhases.NormalRound,
                EnumDevelopmentCardTypes.YearOfPlenty => EnumGamePhases.YearOfPlentyCard
            };

            Session.DevCardPlayedMutation(card);

            return ResultPlayDevCard.Ok(player.ID, card.ID, card.Type, nextPhase);
        }
    }
}