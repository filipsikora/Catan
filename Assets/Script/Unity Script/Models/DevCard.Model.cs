using Catan.Shared.Data;

namespace Catan.Unity.Models
{
    public class DevCardModel
    {
        public int Id { get; set; }
        public EnumDevelopmentCardTypes Type { get; set; }
        public bool IsNew { get; set; }
        public bool IsPlayable { get; set; }

        public DevCardModel(int id, EnumDevelopmentCardTypes type, bool isNew, bool isPlayable)
        {
            Id = id;
            Type = type;
            IsNew = isNew;
            IsPlayable = isPlayable;
        }
    }
}
