using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class VertexClickedCommand : ICommand
    {
        public int VertexId { get; }
        public VertexClickedCommand(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class EdgeClickedCommand : ICommand
    {
        public int EdgeId { get; }
        public EdgeClickedCommand(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public class HexClickedCommand : ICommand
    {
        public int HexId { get; }
        public HexClickedCommand(int hexid)
        {
            HexId = hexid;
        }
    }
}