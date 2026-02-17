using Catan.Core.Snapshots;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;
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

        private EventBus Bus => ManagerGame.Instance.EventBus;

        public void Awake()
        {
            RegisterButton(EnumTradeOfferUIButtons.CancelTradeOffer, CancelTradeButton);
        }

        public void Show(IReadOnlyList<PlayerNameSnapshot> potentialPartnersData)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsChoiceContainer);
            VisualsUI.ClearContainer(CardsReviewContainer);
            VisualsUI.ClearContainer(PlayersButtonsContainer);

            foreach (EnumResourceTypes type in Enum.GetValues(typeof(EnumResourceTypes)))
            {
                CardFactory.DrawResourceCard(type, EnumResourceCardLocation.DesiredTrade, CardsChoiceContainer);
            }

            foreach (var player in potentialPartnersData)
            {
                var buttonObj = Instantiate(PlayerButtonPrefab, PlayersButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = player.Name;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => Bus.Publish(new TradePartnerChosenCommand(player.Id)));
            }
        }

        public void DrawVisualResourceCardInReview(EnumResourceTypes type)
        {
            CardFactory.DrawResourceCard(type, EnumResourceCardLocation.ReviewTrade, CardsReviewContainer);
        }

        public void DestroyVisualResourceCardInReview(EnumResourceTypes type)
        {
            VisualResourceCard card = CardsReviewContainer.GetComponentsInChildren<VisualResourceCard>().First(c => c.Type == type);
            Destroy(card.gameObject);
        }
    }
}
