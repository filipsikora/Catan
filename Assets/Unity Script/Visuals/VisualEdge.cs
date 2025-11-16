using System;
using UnityEngine;

namespace Catan
{
    public class VisualEdge : MonoBehaviour
    {
        public int EdgeId;
        public void OnEdgeClicked()
        {
            var edge = CatanGameManager.Instance.Game.Map.GetEdgeById(EdgeId);
            CatanGameManager.Instance.PhaseHandler.CurrentPhase?.OnEdgeClicked(edge);
        }
    }
}