using Catan.Shared.Communication.Commands;
using UnityEngine;

namespace Catan.Unity.Visuals.Models
{
    public class VisualHex : MonoBehaviour
    {
        public int HexId;

        public void OnHexClicked()
        {
            ManagerGame.Instance.EventBus.Publish(new HexClickedCommand(HexId));
        }    
    }
}