using UnityEngine;
using UnityEngine.UI;
using Catan.Shared.Communication.Commands;

namespace Catan.Unity.Panels
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
            ManagerGame.Instance.EventBus.Publish(new PlayerCountSelectedCommand(count));
        }
    }
}