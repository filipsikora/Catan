using Catan.Shared.Dtos;
using Catan.Unity.Models;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Mappers
{
    public static class PlayerMappers
    {
        public static MyPlayerModel MapPlayerDtoToModel(GameStatePerPlayerDto gameState)
        {
            var player = gameState.Player.Data;
            var resources = gameState.Player.Resources;

            return new MyPlayerModel(player.PlayerId, player.Name, player.BuildingsLeft, player.Points, player.Knights, player.VictoryPoints, player.ExtraPoints, 
                MapDevCardDtoToModel(player.DevCards), resources.PlayerResources);
        }

        public static List<DevCardModel> MapDevCardDtoToModel(List<DevelopmentCardDto> devCardsList)
        {
            return devCardsList.Select(devCard => new DevCardModel(devCard.Id, devCard.Type, devCard.IsNew, devCard.IsPlayable)).ToList();
        }

        public static List<OtherPlayerModel> MapOtherPlayersDtoToModel(OtherPlayersDto otherPlayersList)
        {
            return otherPlayersList.OtherPlayers.Select(otherPlayer => new OtherPlayerModel(otherPlayer.Id, otherPlayer.Name, otherPlayer.ResourceCardsNumber, otherPlayer.ResourceCardsNumber, 
                otherPlayer.VictoryCardsPlayed, otherPlayer.KnightCardsPlayed)).ToList();
        }
    }
}