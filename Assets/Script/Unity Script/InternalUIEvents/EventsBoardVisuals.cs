using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public class PositionsResetUIEvent : IInternalUIEvents { }

    public sealed class VertexHighlightedUIEvent : IInternalUIEvents
    {
        public int VertexId;
        public VertexHighlightedUIEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public sealed class EdgeHighlightedUIEvent : IInternalUIEvents
    {
        public int EdgeId;
        public EdgeHighlightedUIEvent(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public sealed class VertexClickedUIEvent : IInternalUIEvents
    {
        public int VertexId;
        public VertexClickedUIEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public sealed class EdgeClickedUIEvent : IInternalUIEvents
    {
        public int EdgeId;
        public EdgeClickedUIEvent(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public sealed class HexClickedUIEvent : IInternalUIEvents
    {
        public int HexId;
        public HexClickedUIEvent(int hexId)
        {
            HexId = hexId;
        }
    }
}