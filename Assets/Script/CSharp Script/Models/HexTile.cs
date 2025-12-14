#nullable enable
using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Catan
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
