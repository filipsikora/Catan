namespace Catan.Shared.Communication.Events
{
    public class VertexHighlightedEvent
    {
        public int VertexId;
        public VertexHighlightedEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class EdgeHighlightedEvent
    {
        public int EdgeId;
        public EdgeHighlightedEvent(int edgeId)
        {
            EdgeId = edgeId;
        }
    }
}