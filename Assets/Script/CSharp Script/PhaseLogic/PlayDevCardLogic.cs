using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;
using System;

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
            var nextPhase = EnumGamePhases.NormalRound;

            if (!result.Success)
            {
                return ResultPlayDevCard.Fail(result.Reason, player.ID);
            }

            switch (card.Type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    nextPhase = EnumGamePhases.RobberPlacing;
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    nextPhase = EnumGamePhases.MonopolyCard;
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    result = RulesDevCards.CanPlayRoadBuilding(player);

                    if (!result.Success)
                        return ResultPlayDevCard.Fail(result.Reason, player.ID);


                    var roadsAvailable = Math.Min(Session.GetCurrentPlayersRoadsLeft(), 2);

                    Session.CreateRoadBuildingContext(roadsAvailable);

                    nextPhase = EnumGamePhases.RoadBuilding;
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    result = RulesDevCards.CanPlayYearOfPlenty(Session.GetBank(), 2);

                    if (!result.Success)
                        return ResultPlayDevCard.Fail(result.Reason, player.ID);


                    nextPhase = EnumGamePhases.YearOfPlentyCard;
                    break;
            }

            Session.DevCardPlayedMutation(card);

            return ResultPlayDevCard.Ok(player.ID, card.ID, card.Type, nextPhase);
        }
    }
}