using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using Catan.Communication.Signals;

namespace Catan
{

    public class PlayerSelectorUI : MonoBehaviour
    {
        public Button[] playerButtons;

        private void Start()
        {
            for (int i = 0; i < playerButtons.Length; i++)
            {
                int playerCount = i + 2;
                playerButtons[i].onClick.AddListener(() => SelectPlayerCount(playerCount));
            }
        }

        private void SelectPlayerCount(int count)
        {
            ManagerGame.Instance.EventBus.Publish(new PlayerCountSelectedSignal(count));
        }
    }
}