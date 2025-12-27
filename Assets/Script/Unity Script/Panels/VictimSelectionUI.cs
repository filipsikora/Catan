using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Catan.Shared.Communication.Commands;
using Catan.Core.Models;

namespace Catan.Unity.Panels
{
    public class VictimSelectionUI : MonoBehaviour
    {
        public TextMeshProUGUI TitleText;
        public Transform ButtonsContainer;
        public GameObject ButtonPlayerOptionPrefab;

        public void Show(List<Player> potentialVictims)
        {
            gameObject.SetActive(true);

            foreach (Transform child in ButtonsContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var player in potentialVictims)
            {
                var buttonObj = Instantiate(ButtonPlayerOptionPrefab, ButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = player.Name;

                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int victimId = player.ID;

                    ManagerGame.Instance.EventBus.Publish(new VictimChosenCommand(victimId));
                    gameObject.SetActive(false);
                });
            }
        }
    }
}