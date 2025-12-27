using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using Catan.Core.Models;

namespace Catan.Unity.Panels
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

        public void Show(Player offeredPlayer, Player offeringPlayer, ResourceCostOrStock offeredCards, ResourceCostOrStock desiredCards)
        {
            string text = $"{offeredPlayer}, {offeringPlayer} is offering";
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
