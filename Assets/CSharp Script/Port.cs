#nullable enable
using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace Catan
{
    public class Port
    {
        public Vertex VertexA;

        public Vertex VertexB;

        public Player Owner;

        public Edge Edge;

        public EnumResourceTypes? Type;

        public Port(Vertex a, Vertex b, Edge edge)
        {
            VertexA = a;
            VertexB = b;
            Edge = edge;
            Type = null;
        }

    }
}
