using Catan.Unity.Helpers;
using Catan.Unity.Visuals;
using Catan.Shared.Data;
using Catan.Core.Models;
using TMPro;
using UnityEngine;

namespace Catan.Unity.Panels
{
    public class CardTheftUI : MonoBehaviour
    {
        public Transform CardsContainer;
        public FactoryResourceCards CardFactory;
        public TextMeshProUGUI TitleText;

        public void Show(ResourceCostOrStock victimCards)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            foreach (var entry in victimCards.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.VictimHand, CardsContainer, false);
                }
            }
        }
    }
}
