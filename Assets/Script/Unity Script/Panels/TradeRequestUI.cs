using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using System.Collections.Generic;

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

        public void Show(string sellerName, string buyerName, Dictionary<EnumResourceType, int> offered, Dictionary<EnumResourceType, int> desired)
        {
            string text = $"{buyerName}, {sellerName} is offering";
            TradeOfferText.text = text;

            VisualsUI.ClearContainer(OfferedCardsContainer);
            VisualsUI.ClearContainer(DesiredCardsContainer);

            gameObject.SetActive(true);

            foreach (var entry in offered)
            {
                EnumResourceType type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.OfferedTrade, OfferedCardsContainer);
                }
            }

            foreach (var entry in desired)
            {
                EnumResourceType type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.DesiredTrade, DesiredCardsContainer);
                }
            }
        }
    }
}
