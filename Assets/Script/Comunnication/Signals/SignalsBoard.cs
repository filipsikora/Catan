using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;
using JetBrains.Annotations;
using NUnit.Framework.Internal;

namespace Catan.Communication.Signals
{
    public class VertexClickedSignal
    {
        public int VertexId { get; }
        public VertexClickedSignal(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class EdgeClickedSignal
    {
        public int EdgeId { get; }
        public EdgeClickedSignal(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public class HexClickedSignal
    {
        public int HexId { get; }
        public HexClickedSignal(int hexid)
        {
            HexId = hexid;
        }
    }
}
