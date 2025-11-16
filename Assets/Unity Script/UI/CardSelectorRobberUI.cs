using Catan.Catan;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class CardSelectorRobberUI : VisualButton<EnumCardSelectorUIButtons>
    {
        public Transform CardsContainer;
        public ResourceCardFactory CardFactory;

        public TextMeshProUGUI TitleText;

        public Button ConfirmDiscardButton;


        public void Awake()
        {
            RegisterButton(EnumCardSelectorUIButtons.ConfirmDiscard, ConfirmDiscardButton);
        }


        public void Show(Player player, GamePhase phase)
        {
            VisualsUI.ClearContainer(CardsContainer);

            gameObject.SetActive(true);

            foreach (var entry in player.Resources.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    VisualsUI.DrawResourceCard(CardFactory, CardsContainer, type);
                }
            }
        }
    }
}