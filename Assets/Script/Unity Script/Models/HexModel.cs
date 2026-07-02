using Catan.Shared.Data;
using System.Data;

namespace Catan.Unity.Models
{
    public class HexModel
    {
        public int HexId { get; set; }
        public int? HexNumber { get; set; }
        public EnumFieldTypes? FieldType { get; set; }
        public int Q { get; set; }
        public int R { get; set; }

        public HexModel(int hexId, int? hexNumber, EnumFieldTypes? fieldType, int q, int r)
        {
            HexId = hexId;
            HexNumber = hexNumber;
            FieldType = fieldType;
            Q = q;
            R = r;
        }
    }
}
