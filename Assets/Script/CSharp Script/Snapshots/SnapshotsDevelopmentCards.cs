using Catan.Shared.Data;

namespace Catan.Core.Snapshots
{
    public sealed class DevelopmentCardSnapshot
    {
        public int Id { get; }
        public EnumDevelopmentCardTypes Type { get; }
        public bool IsNew { get; }
        public bool IsPlayable { get; }

        public DevelopmentCardSnapshot(int id, EnumDevelopmentCardTypes type, bool isNew, bool isPlayable)
        {
            Id = id;
            Type = type;
            IsNew = isNew;
            IsPlayable = isPlayable;
        }
    }
}