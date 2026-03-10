using Catan.Application.Snapshots;
using Catan.Unity.Helpers;
using Catan.Unity.Data;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

        public void Show(IReadOnlyList<DevelopmentCardSnapshot> cards)
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