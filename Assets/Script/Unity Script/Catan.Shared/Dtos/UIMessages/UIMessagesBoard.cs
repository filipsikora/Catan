using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class VertexHighlightedDto : IUiMessageDto
    {
        public int VertexId;
        public VertexHighlightedDto(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public sealed class EdgeHighlightedDto : IUiMessageDto
    {
        public int EdgeId;
        public EdgeHighlightedDto(int edgeId)
        {
            EdgeId = edgeId;
        }
    }
}