#nullable enable
using Catan.Shared.Data;

namespace Catan.Core.Models
{
    public class DevelopmentCard
    {
        public bool IsNew = true;

        public Player? Owner = null;

        public EnumDevelopmentCardTypes Type;

        public bool IsUsed = false;

        public int ID;

        public static readonly ResourceCostOrStock Cost = new ResourceCostOrStock(1, 0, 1, 1, 0);


        public DevelopmentCard(EnumDevelopmentCardTypes type, int id)
        {
            Type = type;
            ID = id;
        }
    }
}
