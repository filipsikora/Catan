using Catan.Shared.Communication.Commands;
using UnityEngine;

namespace Catan.Unity.Visuals.Models
{
    public class VisualEdge : MonoBehaviour
    {
        public int EdgeId;
        public void OnEdgeClicked()
        {
            ManagerGame.Instance.EventBus.Publish(new EdgeClickedCommand(EdgeId));
        }
    }
}