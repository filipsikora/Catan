using Catan.Core;
using Catan.Core.Models;
using Catan.Core.Queries.Interfaces;
using Catan.Core.Results;
using Catan.Core.Snapshots;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Controllers
{
    public sealed class Facade
    {
        private readonly GameSession _session;

        private readonly IBoardQueryService _boardQuery;
        private readonly IDevCardsQueryService _devCardQuery;
        private readonly IPlayersQueryService _playersQuery;
        private readonly IResourcesQueryService _resourcesQuery;
        private readonly ITradeQueryService _tradeQuery;
        private readonly ITurnsQueryService _turnsQuery;

        public Facade(GameSession session, IBoardQueryService boardQuery, IDevCardsQueryService devcardQuery, IPlayersQueryService playersQuery, 
            IResourcesQueryService resourcesQuery, ITradeQueryService tradeQuery, ITurnsQueryService turnsQuery)
        {
            _session = session;
            _boardQuery = boardQuery;
            _devCardQuery = devcardQuery;
            _playersQuery = playersQuery;
            _resourcesQuery = resourcesQuery;
            _tradeQuery = tradeQuery;
            _turnsQuery = turnsQuery;
        }

        // getters//

        public EnumGamePhases GetCurrentPhase() => _session.GetCurrentPhase();
        public EnumGamePhases GetNextPhaseFromAfterRoll() => _session.GetNextPhaseFromAfterRoll();
        public EnumGamePhases? GetNextPhaseAfterDiscarding() => _session.GetNextPhaseAfterDiscarding();

        public int GetCurrentPlayerTradeRatio(EnumResourceTypes resource) => _session.GetCurrentPlayerTradeRatio(resource);
        public bool PlayerHasEnoughResources(int playerAmount, int neededAmount) => _session.PlayerHasEnoughResources(playerAmount, neededAmount);
        public int GetCurrentPlayersRoadsLeft() => _session.GetCurrentPlayersRoadsLeft();
        public int GetCurrentPlayerResourceAmount(EnumResourceTypes resource) => _session.GetCurrentPlayerResourceAmount(resource);
        public int GetCurrentPlayerId() => _session.GetCurrentPlayerId();

        public List<int> GetAdjacentToHexPlayersIds(int hexId) => _session.GetAdjacentToHexPlayersIds(hexId);

        public List<int> GetPossibleVictimsIds() => _session.GetPossibleVictimsIds();

        public int GetNextToDiscardId() => _session.GetNextToDiscardId();

        public bool CanPlayerDiscard(ResourceCostOrStock resourcesSelected, int discardingPlayerId) => _session.CanPlayerDiscard(resourcesSelected, discardingPlayerId);

        public int GetVictimId() => _session.GetVictimId();

        public bool GetAfterRoll() => _session.GetAfterRoll();

        public int GetLastPlacedVillagePositionId() => _session.GetLastPlacedVillagePositionId();

        public int GetRoadsLeftToBuild() => _session.GetRoadsLeftToBuild();

        public bool CheckIfCardsSelected(ResourceCostOrStock resources) => _session.CheckIfCardsSelected(resources);

        public bool CheckIfExactCardsAmountSelected(ResourceCostOrStock resources, int amount) => _session.CheckIfExactCardsAmountSelected(resources, amount);

        public int GetDesertHexId() => _session.GetDesertHexId();

        // use cases//

        public ResultBankTrade UseBankTrade(EnumResourceTypes offered, EnumResourceTypes desired) => _session.UseBankTrade(offered, desired);
        public ResultBlockHex UseBlockHex(int hexId) => _session.UseBlockHex(hexId);
        public ResultCondition UseSelectVictim(int victimId) => _session.UseSelectVictim(victimId);
        public ResultRollDice UseRollDice() => _session.UseRollDice();
        public ResultCondition UseDiscard(int discardingPlayerId, ResourceCostOrStock resourcesSelected) => _session.UseDiscard(discardingPlayerId, resourcesSelected);
        public ResultStealResource UseSteal(int victimId, EnumResourceTypes resource) => _session.UseSteal(victimId, resource);
        public ResultPlayDevCard UseDevCard(int cardId) => _session.UseDevCard(cardId);
        public ResultBuildInitialRoad UseBuildInitialRoad(int edgeId, int vertexId) => _session.UseBuildInitialRoad(edgeId, vertexId);
        public ResultBuildInitialVillage UseBuildInitialVillage(int vertexId) => _session.UseBuildInitialVillage(vertexId);
        public ResultBuildRoad UseBuildRoad(int edgeId) => _session.UseBuildRoad(edgeId);
        public ResultBuildVillage UseBuildVillage(int vertexId) => _session.UseBuildVillage(vertexId);
        public ResultBuildFreeRoad UseBuildFreeRoad(int edgeId) => _session.UseBuildFreeRoad(edgeId);
        public ResultFinishTurn UseFinishTurn() => _session.UseFinishTurn();
        public ResultMonopolyCard UseMonopolyCard(EnumResourceTypes resource) => _session.UseMonopolyCard(resource);
        public ResultBuyDevCard UseBuyDevCard() => _session.UseBuyDevCard();
        public ResultUpgradeVillage UseUpgradeVillage(int vertexId) => _session.UseUpgradeVillage(vertexId);
        public ResultCondition UsePrepareTrade(ResourceCostOrStock offered) => _session.UsePrepareTrade(offered);
        public ResultPlayerTrade UseOfferTrade(int buyerId, ResourceCostOrStock desired) => _session.UseOfferTrade(buyerId, desired);
        public ResultPlayerTrade UseReactToTrade() => _session.UseReactToTrade();
        public ResultYearOfPlenty UseYearOfPlenty(ResourceCostOrStock resources) => _session.UseYearOfPlenty(resources);

        // queries //

        public BoardSnapshot GetBoardData() => _boardQuery.GetBoardData();
        public PlayerDataSnapshot GetPlayersData(int playerId) => _playersQuery.GetPlayersData(playerId);
        public PlayerResourcesSnapshot GetPlayersCards(int playerId) => _playersQuery.GetPlayersCards(playerId);
        public ResourcesAvailabilitySnapshot GetResourcesAvailability() => _resourcesQuery.GetResourcesAvailability();
        public TurnDataSnapshot GetTurnData() => _turnsQuery.GetTurnData();
    }
}