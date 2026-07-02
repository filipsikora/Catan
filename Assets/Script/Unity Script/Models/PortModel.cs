using Catan.Shared.Data;

namespace Catan.Unity.Models
{
    public class PortModel
    {
        public int EdgeId { get; set; }
        public EnumResourceType? Type { get; set; }

        public PortModel(int edgeId, EnumResourceType? type)
        {
            EdgeId = edgeId;
            Type = type;
        }
    }
}
