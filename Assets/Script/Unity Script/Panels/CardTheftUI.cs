using Catan.Unity.Helpers;
using Catan.Unity.Visuals;
using Catan.Shared.Data;
using TMPro;
using UnityEngine;
using Catan.Shared.Dtos;

namespace Catan.Unity.Panels
{
    public class CardTheftUI : MonoBehaviour
    {
        public Transform CardsContainer;
        public FactoryResourceCards CardFactory;
        public TextMeshProUGUI TitleText;

        public void Show(PlayerResourcesDto resourcesSnapshot)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            foreach (var entry in resourcesSnapshot.PlayerResources)
            {
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    CardFactory.DrawResourceCard(entry.Key, EnumResourceCardLocation.VictimHand, CardsContainer, false);
                }
            }
        }
    }
}