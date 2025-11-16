using System;
using UnityEngine;

namespace Catan
{
    public class VisualVertex : MonoBehaviour
    {
        public int VertexId;

        public void OnVertexClicked()
        {
            var vertex = CatanGameManager.Instance.Game.Map.GetVertexById(VertexId);
            CatanGameManager.Instance.PhaseHandler.CurrentPhase?.OnVertexClicked(vertex);
        }
    }
}