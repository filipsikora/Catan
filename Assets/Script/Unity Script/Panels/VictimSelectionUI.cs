using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Catan.Shared.Communication.Commands;
using Catan.Core.Snapshots;

namespace Catan.Unity.Panels
{
    public class VictimSelectionUI : MonoBehaviour
    {
        public TextMeshProUGUI TitleText;
        public Transform ButtonsContainer;
        public GameObject ButtonPlayerOptionPrefab;

        public void Show(IReadOnlyList<PlayerNameSnapshot> potentialVictimsData)
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
                    ManagerGame.Instance.EventBus.Publish(new VictimChosenCommand(victimData.Id));
                    gameObject.SetActive(false);
                });
            }
        }
    }
}