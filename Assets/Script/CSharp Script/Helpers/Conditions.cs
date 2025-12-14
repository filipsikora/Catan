#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Catan
{
    public static class Conditions
    {
        public static bool PositionExists(int id, Func<int, IPositionData?> getPositionFunc)
        {
            var position = getPositionFunc(id);
            if (position == null)
            {
                UnityEngine.Debug.Log("This position doesn't exist.");
                return false;
            }
            return true;
        }

        public static bool NoSettlementsInRange(Vertex vertex)
        {

            foreach (var position in vertex.NeighbourVertices)
            {
                if (position.IsOwned)
                {
                    UnityEngine.Debug.Log("There is a settlement too close to this position.");
                    return false;
                }
            }

            return true;
        }

        public static bool HasVillage(Player player, Vertex vertex)
        {

            if (vertex.Owner == player && vertex.HasVillage)
            {
                return true;
            }

            UnityEngine.Debug.Log("You don't own a village on this position.");
            return false;
        }

        public static bool CanAfford(ResourceCostOrStock playerResources, ResourceCostOrStock cost)
        {

            if (!playerResources.HasEnoughCards(cost))
            {
                UnityEngine.Debug.Log("You can't afford it");
                return false;
            }

            return true;
        }

        public static bool HasAvailable<T>(Player player)
            where T : Building, IBuildingData
        {
            int max = BuildingDataRegistry.MaxPerPlayer[typeof(T)];

            if (player.BuildingCount<T>() < max)
            {
                return true;
            }

            UnityEngine.Debug.Log("No more buildings of this type available.");
            return false;
        }

        public static bool IsNotOwned(IPositionData position)
        {

            if (position.Owner != null)
            {
                UnityEngine.Debug.Log("Position already occupied.");
                return false;
            }

            return true;
        }

        public static bool HasAccessToPosition(Player player, IPositionData position)
        {

            if (position.AccessibleByPlayer(player))
            {
                return true;
            }

            UnityEngine.Debug.Log("No access to this position.");
            return false;
        }
    }
}
 