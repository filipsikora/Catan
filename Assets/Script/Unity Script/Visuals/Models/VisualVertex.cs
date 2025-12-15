using Catan.Communication.Signals;
using System;
using UnityEngine;

namespace Catan
{
    public class VisualVertex : MonoBehaviour
    {
        public int VertexId;

        public void OnVertexClicked()
        {
            ManagerGame.Instance.EventBus.Publish(new VertexClickedSignal(VertexId));
        }
    }
}