using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
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
}