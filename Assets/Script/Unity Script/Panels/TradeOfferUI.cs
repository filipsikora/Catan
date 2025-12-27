using Catan.Shared.Data;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

        public void Show(Dictionary<int, string> potentialPartnersIds)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsChoiceContainer);
            VisualsUI.ClearContainer(CardsReviewContainer);
            VisualsUI.ClearContainer(PlayersButtonsContainer);

            foreach (var key in ManagerGame.Instance.Game.Bank.ResourceDictionary.Keys)
            {
                CardFactory.DrawResourceCard(key, EnumResourceCardLocation.DesiredTrade, CardsChoiceContainer);
            }

            foreach (var (value, key) in potentialPartnersIds)
            {
                var buttonObj = Instantiate(PlayerButtonPrefab, PlayersButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = potentialPartnersIds[value];
                buttonObj.GetComponent<Button>().onClick.AddListener(() => Bus.Publish(new TradePartnerChosenCommand(value)));
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
