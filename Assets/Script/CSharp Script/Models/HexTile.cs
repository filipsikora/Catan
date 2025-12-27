#nullable enable
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Models
{
    public class HexTile
    {
        public int Q { get; }

        public int R { get; }

        public int S => -Q - R;

        public int Id { get; set; }

        public float X { get; set; }

        public float Y { get; set; }


        public EnumFieldTypes? FieldType { get; set; } = null;

        public int? FieldNumber { get; set; } = null;

        public List<Vertex> AdjacentVertices { get; set; } = new List<Vertex>();

        public bool isBlocked = false;


        public HexTile(int q, int r, int id)
        {
            Q = q; 
            R = r;
            Id = id;
        }


        public override string ToString()
        {
            return $"Hex ({Q}, {R}) - {FieldNumber}, {FieldType}";
        }

        public EnumResourceTypes? GetResourceType()
        {
            return FieldType switch
            {
                EnumFieldTypes.Wheat => EnumResourceTypes.Wheat,
                EnumFieldTypes.Wood => EnumResourceTypes.Wood,
                EnumFieldTypes.Wool => EnumResourceTypes.Wool,
                EnumFieldTypes.Stone => EnumResourceTypes.Stone,
                EnumFieldTypes.Clay => EnumResourceTypes.Clay,
                EnumFieldTypes.Desert => null,
                _ => null
            };
        }

    }
}
