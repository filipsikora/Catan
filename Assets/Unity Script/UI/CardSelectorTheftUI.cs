using Catan.Catan;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class CardSelectorTheftUI : MonoBehaviour
    {
        public Transform CardsContainer;
        public ResourceCardFactory CardFactory;

        public TextMeshProUGUI TitleText;

        private GamePhase CurrentPhase;


        public void Show(Player victim, CardStealing phase)
        {
            CurrentPhase = phase;
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            foreach (var entry in victim.Resources.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    VisualsUI.DrawResourceCard(CardFactory, CardsContainer, type, visible: false);
                }
            }
        }
    }
}
