using Catan.Shared.Data;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Catan.Unity.Models;

namespace Catan.Unity.Panels
{
    public class TradeOfferUI : VisualButton<EnumTradeOfferUIButtons>
    {
        public Transform CardsChoiceContainer;
        public Transform CardsReviewContainer;
        public Transform PlayersButtonsContainer;

        public FactoryResourceCards CardFactory;

        public Button CancelTradeButton;
        public GameObject PlayerButtonPrefab;

        public void Awake()
        {
            RegisterButton(EnumTradeOfferUIButtons.CancelTradeOffer, CancelTradeButton);
        }

        public void Show(IReadOnlyList<OtherPlayerModel> potentialPartnersData, EventBus bus)
        {
            gameObject.SetActive(true);
            PlayersButtonsContainer.gameObject.SetActive(false);

            VisualsUI.ClearContainer(CardsChoiceContainer);
            VisualsUI.ClearContainer(CardsReviewContainer);
            VisualsUI.ClearContainer(PlayersButtonsContainer);

            foreach (EnumResourceType type in Enum.GetValues(typeof(EnumResourceType)))
            {
                CardFactory.DrawResourceCard(type, EnumResourceCardLocation.DesiredTrade, CardsChoiceContainer);
            }

            foreach (var player in potentialPartnersData)
            {
                var buttonObj = Instantiate(PlayerButtonPrefab, PlayersButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = player.Name;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => bus.Publish(new PlayerClickedUIEvent(player.Id)));
            }
        }

        public void DrawVisualResourceCardInReview(EnumResourceType type)
        {
            CardFactory.DrawResourceCard(type, EnumResourceCardLocation.ReviewTrade, CardsReviewContainer);
        }

        public void DestroyVisualResourceCardInReview(EnumResourceType type)
        {
            VisualResourceCard card = CardsReviewContainer.GetComponentsInChildren<VisualResourceCard>().First(c => c.Type == type);
            Destroy(card.gameObject);
        }
    }
}
