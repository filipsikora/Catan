using Catan.Core;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Controllers
{
    public sealed class Facade
    {
        private GameSession _session;

        public Facade(GameSession session)
        {
            _session = session;
        }

        // getters//

        public EnumGamePhases GetCurrentPhase() => _session.GetCurrentPhase();

        public int GetCurrentPlayerTradeRatio(EnumResourceTypes resource) => _session.GetCurrentPlayerTradeRatio(resource);
        public int GetCurrentPlayersRoadsLeft() => _session.GetCurrentPlayersRoadsLeft();
        public int GetCurrentPlayerResourceAmount(EnumResourceTypes resource) => _session.GetCurrentPlayerResourceAmount(resource);
        public int GetCurrentPlayerId() => _session.GetCurrentPlayerId();

        public List<int> GetAdjacentToHexPlayersIds(int hexId) => _session.GetAdjacentToHexPlayersIds(hexId);

        public List<int> GetPossibleVictimsIds() => _session.GetPossibleVictimsIds();

        public int GetPlayersToDiscardCount() => _session.GetPlayersToDiscardCount();

        public int GetNextToDiscardId() => _session.GetNextToDiscardId();

        public bool CanPlayerDiscard(ResourceCostOrStock resourcesSelected, int discardingPlayerId) => _session.CanPlayerDiscard(resourcesSelected, discardingPlayerId);

        public int GetVictimId() => _session.GetVictimId();

        public bool GetAfterRoll() => _session.GetAfterRoll();

        public int GetLastPlacedVillagePositionId() => _session.GetLastPlacedVillagePositionId();

        public int GetRoadsLeftToBuild() => _session.GetRoadsLeftToBuild();

        // use cases//

        public ResultBankTrade UseBankTrade(EnumResourceTypes offered, EnumResourceTypes desired) => _session.UseBankTrade(offered, desired);
        public ResultBlockHex UseBlockHex(int hexId) => _session.UseBlockHex(hexId);
        public ResultCondition UseSelectVictim(int victimId) => _session.UseSelectVictim(victimId);
        public ResultRollDice UseRollDice() => _session.UseRollDice();
        public ResultCondition UseDiscard(int discardingPlayerId, ResourceCostOrStock resourcesSelected) => _session.UseDiscard(discardingPlayerId, resourcesSelected);
        public ResultStealResource UseSteal(int victimId, EnumResourceTypes resource) => _session.UseSteal(victimId, resource);
        public Result<DevelopmentCard> UseDevCard(int cardId) => _session.UseDevCard(cardId);
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
        public ResultCondition UsePrepareRoadBuilding() => _session.UsePrepareRoadBuilding();
    }
}