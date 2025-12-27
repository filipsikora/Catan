using Catan.Shared.Data;
using Catan.Core.Models;
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

        public void Show(List<int> playerCardsById, bool afterRoll)
        {
            CancelDevelopmentCardsButton.gameObject.SetActive(true);
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            if (afterRoll)
            {
                foreach (var id in playerCardsById)
                {
                    DevelopmentCard card = ManagerGame.Instance.Game.DevelopmentCardsDeckAll.Find(c => c.ID == id);

                    DevelopmentCardFactory.DrawDevelopmentCard(card, CardsContainer);
                }
            }

            else
            {
                foreach (var id in playerCardsById)
                {
                    DevelopmentCard card = ManagerGame.Instance.Game.DevelopmentCardsDeckAll.Find(c => c.ID == id);

                    if (card.Type == EnumDevelopmentCardTypes.Knight)
                    {
                        DevelopmentCardFactory.DrawDevelopmentCard(card, CardsContainer);
                    }
                }
            }
        }
    }
}