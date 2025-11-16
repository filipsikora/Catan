using System;
using UnityEngine;

namespace Catan
{
    public class VisualHex : MonoBehaviour
    {
        public int HexId;

        public void OnHexClicked()
        {
            var hex = CatanGameManager.Instance.Game.Map.GetHexById(HexId);
            CatanGameManager.Instance.PhaseHandler.CurrentPhase?.OnHexClicked(hex);
        }    
    }
}