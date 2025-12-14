using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Catan.Communication.Signals;

namespace Catan
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
                    int victimId = ManagerGame.Instance.Game.PlayerList.IndexOf(player);

                    ManagerGame.Instance.EventBus.Publish(new VictimChosenSignal(victimId));
                    gameObject.SetActive(false);
                });
            }
        }
    }
}