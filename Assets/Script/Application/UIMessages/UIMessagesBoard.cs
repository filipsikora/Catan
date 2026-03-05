using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class VertexHighlightedMessage : IUIMessages
    {
        public int VertexId;
        public VertexHighlightedMessage(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public sealed class EdgeHighlightedMessage : IUIMessages
    {
        public int EdgeId;
        public EdgeHighlightedMessage(int edgeId)
        {
            EdgeId = edgeId;
        }
    }
}
