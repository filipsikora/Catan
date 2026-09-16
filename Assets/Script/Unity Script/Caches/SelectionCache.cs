namespace Catan.Unity.Caches
{
    public class SelectionCache
    {
        public int? SelectedVertexId { get; private set; }
        public int? SelectedEdgeId { get; private set; }

        public SelectionCache()
        {
            SelectedVertexId = null;
            SelectedEdgeId = null;
        }

        public void SelectVertex(int vertexId)
        {
            SelectedEdgeId = null;
            SelectedVertexId = vertexId;
        }

        public void SelectEdge(int edgeId)
        {
            SelectedVertexId = null;
            SelectedEdgeId = edgeId;
        }

        public void Clear()
        {
            SelectedEdgeId = null;
            SelectedVertexId = null;
        }
    }
}
