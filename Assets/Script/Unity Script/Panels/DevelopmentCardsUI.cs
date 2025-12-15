using Catan.Catan;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
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

        public void Show(Player player, bool rolled)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsContainer);

            if (rolled)
            {
                foreach (var id in player.DevelopmentCardsByID)
                {
                    DevelopmentCard card = ManagerGame.Instance.Game.DevelopmentCardsDeckAll.Find(c => c.ID == id);

                    DevelopmentCardFactory.DrawDevelopmentCard(card, CardsContainer);
                }
            }

            else
            {
                foreach (var id in player.DevelopmentCardsByID)
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