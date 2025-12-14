using Catan.Communication.Signals;
using System;
using UnityEngine;

namespace Catan
{
    public class VisualEdge : MonoBehaviour
    {
        public int EdgeId;
        public void OnEdgeClicked()
        {
            ManagerGame.Instance.EventBus.Publish(new EdgeClickedSignal(EdgeId));
        }
    }
}