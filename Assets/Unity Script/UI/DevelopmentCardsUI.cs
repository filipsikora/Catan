using Catan.Catan;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class DevelopmentCardsUI : VisualButton<EnumDevelopmentCardsUIButtons>
    {
        public Transform CardsContainer;
        public DevelopmentCardFactory CardFactory;
        public Button CancelDevelopmentCardsButton;

        public void Show(Player player)
        {
            gameObject.SetActive(true);
            RegisterButton(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards, CancelDevelopmentCardsButton);

            VisualsUI.ClearContainer(CardsContainer);

            foreach (var card in player.DevelopmentCards)
            {
                VisualsUI.DrawDevelopmentCard(CardFactory, CardsContainer, card);
            }
        }
    }
}