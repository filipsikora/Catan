using Catan.Shared.Dtos;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Models;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Catan.Unity.Panels
{
    public class DevelopmentCardsUI : VisualButton<EnumDevelopmentCardsUIButtons>
    {
        public Transform CardsContainer;
        public FactoryDevelopmentCards DevelopmentCardFactory;
        public Button CancelDevelopmentCardsButton;

        public void Awake()
        {
            RegisterButton(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards, CancelDevelopmentCardsButton);
        }

        public void Show(IReadOnlyList<DevCardModel> cards)
        {
            CancelDevelopmentCardsButton.gameObject.SetActive(true);
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            foreach (var card in cards)
            {
                DevelopmentCardFactory.DrawDevelopmentCard(card, CardsContainer);
            }
        }
    }
}