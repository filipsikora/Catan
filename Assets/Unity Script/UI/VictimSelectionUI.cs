using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace Catan
{
    public class VictimSelectionUI : MonoBehaviour
    {
        public TextMeshProUGUI TitleText;

        public Transform ButtonsContainer;

        public GameObject ButtonPlayerOptionPrefab;

        private GamePhase currentPhase;

        public void Show(List<Player> potentialVictims, GamePhase phase)
        {
            currentPhase = phase;
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
                    OnPlayerSelected(player);
                });
            }
        }

        public void OnPlayerSelected(Player victim)
        {
            gameObject.SetActive(false);
            currentPhase.OnVictimChosen(victim);
        }
    }
}