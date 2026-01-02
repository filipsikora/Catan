#nullable enable
using System;
using Catan.Shared.Data;
using Catan.Shared.Results;
using Catan.Core.Interfaces;
using Catan.Core.Models;

namespace Catan.Core.Helpers
{
    public static class Conditions
    {
        public static ResultCondition PositionExists(int id, Func<int, IPositionData?> getPositionFunc)
        {
            var position = getPositionFunc(id);

            if (position == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }
            return ResultCondition.Ok();
        }

        public static ResultCondition NoSettlementsInRange(Vertex vertex)
        {
            foreach (var position in vertex.NeighbourVertices)
            {
                if (position.IsOwned)
                {
                    return ResultCondition.Fail(ConditionFailureReason.TooCloseToSettlement);
                }
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition HasVillage(Player player, Vertex vertex)
        {
            if (vertex.Owner == player && vertex.HasVillage)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NotOwner);
        }

        public static ResultCondition CanAfford(ResourceCostOrStock playerResources, ResourceCostOrStock cost)
        {
            if (!playerResources.HasEnoughCards(cost))
                return ResultCondition.Fail(ConditionFailureReason.CannotAfford);

            return ResultCondition.Ok();
        }

        public static ResultCondition HasAvailable<T>(Player player)
            where T : Building, IBuildingData
        {
            int max = BuildingDataRegistry.MaxPerPlayer[typeof(T)];

            if (player.BuildingCount<T>() < max)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NoBuildingsAvailable);
        }

        public static ResultCondition IsNotOwned(IPositionData position)
        {
            if (position.Owner != null)
            {
                return ResultCondition.Fail(ConditionFailureReason.PositionOccupied);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition HasAccessToPosition(Player player, IPositionData position)
        {
            if (position.AccessibleByPlayer(player))
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NoAccess);
        }

        public static ResultCondition CanPlayDevCard(Player player, DevelopmentCard card, bool afterRoll)
        {
            if (card == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            if (card.Owner != player)
            {
                return ResultCondition.Fail(ConditionFailureReason.NotOwner);
            }

            if (card.IsNew)
            {
                return ResultCondition.Fail(ConditionFailureReason.CardIsNew);
            }

            if (!(card.Type == EnumDevelopmentCardTypes.Knight || afterRoll))
            {
                return ResultCondition.Fail(ConditionFailureReason.NotKnightOrAfterRoll);
            }

            if (card.IsUsed)
            {
                return ResultCondition.Fail(ConditionFailureReason.AlreadyUsed);
            }

            return ResultCondition.Ok();
        }
    }
}
 