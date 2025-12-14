using Catan.Catan;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class CardTheftUI : MonoBehaviour
    {
        public Transform CardsContainer;
        public FactoryResourceCards CardFactory;
        public TextMeshProUGUI TitleText;

        public void Show(Player victim)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            foreach (var entry in victim.Resources.ResourceDictionary)
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
