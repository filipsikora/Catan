using QuikGraph;
using Catan.Core.Models;

namespace Catan.Core.Helpers
{
    public class EdgeAdapter : IEdge<Vertex>
    {
        public Vertex Source { get; }
        public Vertex Target { get; }
        public Edge OriginalEdge { get; }

        public EdgeAdapter(Vertex a, Vertex b, Edge original)
        {
            Source = a;
            Target = b;
            OriginalEdge = original;
        }
    }
}