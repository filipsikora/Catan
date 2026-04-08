using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Shared.Dtos;

namespace Catan.Unity.Panels
{
    public class VictimSelectionUI : MonoBehaviour
    {
        public TextMeshProUGUI TitleText;
        public Transform ButtonsContainer;
        public GameObject ButtonPlayerOptionPrefab;

        private EventBus _bus;

        public void Initialize(EventBus bus)
        {
            _bus = bus;
        }

        public void Show(IReadOnlyList<PlayerNameDto> potentialVictimsData)
        {
            gameObject.SetActive(true);

            foreach (Transform child in ButtonsContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var victimData in potentialVictimsData)
            {
                var buttonObj = Instantiate(ButtonPlayerOptionPrefab, ButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = victimData.Name;

                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    _bus.Publish(new PlayerClickedUIEvent(victimData.Id));
                    gameObject.SetActive(false);
                });
            }
        }
    }
}