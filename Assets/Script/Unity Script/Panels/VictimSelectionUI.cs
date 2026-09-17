using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Models;

namespace Catan.Unity.Panels
{
    public class VictimSelectionUI : MonoBehaviour
    {
        public TextMeshProUGUI TitleText;
        public Transform ButtonsContainer;
        public GameObject ButtonPlayerOptionPrefab;

        public void Show(IReadOnlyList<OtherPlayerModel> potentialVictims, EventBus bus)
        {
            gameObject.SetActive(true);

            foreach (Transform child in ButtonsContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var victimData in potentialVictims)
            {
                var buttonObj = Instantiate(ButtonPlayerOptionPrefab, ButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = victimData.Name;

                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    bus.Publish(new PlayerClickedUIEvent(victimData.Id));
                    gameObject.SetActive(false);
                });
            }
        }
    }
}