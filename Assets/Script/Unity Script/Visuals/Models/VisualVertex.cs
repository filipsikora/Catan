using Catan.Shared.Communication.Commands;
using UnityEngine;

namespace Catan.Unity.Visuals.Models
{
    public class VisualVertex : MonoBehaviour
    {
        public int VertexId;

        public void OnVertexClicked()
        {
            Debug.Log($"[CLICK] Vertex {VertexId} clicked");
            ManagerGame.Instance.EventBus.Publish(new VertexClickedCommand(VertexId));
        }
    }
}