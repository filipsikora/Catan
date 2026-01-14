using Catan.Core.Engine;
using Catan.Core.Interfaces;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;
using System;

namespace Catan.Core.Conditions
{
    public static class ConditionsMap
    {
        public static ResultCondition MapExists(HexMap map)
        {
            if (map == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition PositionExists(int id, Func<int, IPositionData?> getPositionFunc)
        {
            var position = getPositionFunc(id);

            if (position == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }
            return ResultCondition.Ok();
        }

        public static ResultCondition HexExists(HexTile hex)
        {
            if (hex == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition IsNotBlocked(HexTile hex)
        {
            if (hex.isBlocked)
            {
                return ResultCondition.Fail(ConditionFailureReason.HexAlreadyBlocked);
            }

            return ResultCondition.Ok();
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
    }
}