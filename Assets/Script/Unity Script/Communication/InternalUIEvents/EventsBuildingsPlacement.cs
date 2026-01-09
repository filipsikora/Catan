namespace Core.Unity.Communication.InternalUIEvents
{
    public class VillagePlacedUIEvent
    {
        public int VertexId;
        public int OwnerId;

        public VillagePlacedUIEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
    public class RoadPlacedUIEvent
    {
        public int EdgeId;
        public int OwnerId;

        public RoadPlacedUIEvent(int edgeId, int ownerId)

        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public class TownPlacedUIEvent
    {
        public int VertexId;
        public int OwnerId;

        public TownPlacedUIEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}
