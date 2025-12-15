using Catan.Communication.Signals;
using System;
using UnityEngine;

namespace Catan
{
    public class VisualHex : MonoBehaviour
    {
        public int HexId;

        public void OnHexClicked()
        {
            ManagerGame.Instance.EventBus.Publish(new HexClickedSignal(HexId));
        }    
    }
}