using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Catan.Catan;
using JetBrains.Annotations;

namespace Catan
{
    public class TradeRequestUI : VisualButton<EnumTradeRequestUIButtons>
    {
        public Button AcceptTradeButton;
        public Button RefuseTradeButton;

        public TextMeshProUGUI TradeOfferText;

        public Transform OfferedCardsContainer;
        public Transform DesiredCardsContainer;

        public FactoryResourceCards CardFactory;

        public void Awake()
        {
            RegisterButton(EnumTradeRequestUIButtons.AcceptTradeRequest, AcceptTradeButton);
            RegisterButton(EnumTradeRequestUIButtons.RefuseTradeRequest, RefuseTradeButton);
        }

        public void Show(Player offeredPlayer, ResourceCostOrStock offeredCards, ResourceCostOrStock desiredCards)
        {
            Player offeringPLayer = ManagerGame.Instance.Game.CurrentPlayer;

            string text = $"{offeredPlayer}, {offeringPLayer} is offering";
            TradeOfferText.text = text;

            VisualsUI.ClearContainer(OfferedCardsContainer);
            VisualsUI.ClearContainer(DesiredCardsContainer);

            gameObject.SetActive(true);

            foreach (var entry in offeredCards.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.OfferedTrade, OfferedCardsContainer);
                }
            }

            foreach (var entry in desiredCards.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.DesiredTrade, DesiredCardsContainer);
                }
            }
        }
    }
}
