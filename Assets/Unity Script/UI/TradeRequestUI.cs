using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Catan.Catan;
using JetBrains.Annotations;

namespace Catan
{
    public class TradeRequestUI : MonoBehaviour
    {
        public Button AcceptTradeButton;
        public Button RefuseTradeButton;

        public TextMeshProUGUI TradeOfferText;

        public Transform OfferedCardsContainer;
        public Transform DesiredCardsContainer;

        public ResourceCardFactory CardFactory;

        public Action<bool> OnTradeAcceptedOrRefused;

        public void Show(Player offeredPlayer, ResourceCostOrStock offeredCards, ResourceCostOrStock desiredCards)
        {
            Player offeringPLayer = CatanGameManager.Instance.Game.currentPlayer;

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
                    VisualsUI.DrawResourceCard(CardFactory, OfferedCardsContainer, type, visible: true);
                }
            }

            foreach (var entry in desiredCards.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    VisualsUI.DrawResourceCard(CardFactory, DesiredCardsContainer, type, visible: true);
                }
            }

            AcceptTradeButton.onClick.RemoveAllListeners();
            AcceptTradeButton.onClick.AddListener(() => OnTradeAcceptedOrRefused?.Invoke(true));

            RefuseTradeButton.onClick.RemoveAllListeners();
            RefuseTradeButton.onClick.AddListener(() => OnTradeAcceptedOrRefused?.Invoke(false));
        }
    }
}
