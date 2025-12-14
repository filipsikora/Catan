using Catan.Catan;


namespace Catan.Core
{
    public class ResourceCard
    {
        public EnumResourceTypes Type { get; }
        public EnumResourceCardLocation Location { get; set; }
        public bool IsSelected { get; private set; } = false;

        public ResourceCard(EnumResourceTypes type, EnumResourceCardLocation location)
        {
            Type = type;
            Location = location;
        }

        public void Toggle()
        {
            IsSelected = !IsSelected;
        }
    }
}