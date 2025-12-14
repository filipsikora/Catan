using Catan.Catan;
using Catan.Communication;
using Catan.Communication.Signals;
using System;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
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
        private GameState Game => ManagerGame.Instance.Game;

        public void Awake()
        {
            RegisterButton(EnumTradeOfferUIButtons.CancelTradeOffer, CancelTradeButton);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsChoiceContainer);
            VisualsUI.ClearContainer(CardsReviewContainer);
            VisualsUI.ClearContainer(PlayersButtonsContainer);

            foreach (var key in ManagerGame.Instance.Game.Bank.ResourceDictionary.Keys)
            {
                CardFactory.DrawResourceCard(key, EnumResourceCardLocation.DesiredTrade, CardsChoiceContainer);
            }

            foreach (var player in Game.PlayerList.Where(p => p != Game.CurrentPlayer))
            {
                int playerId = player.ID;

                var buttonObj = Instantiate(PlayerButtonPrefab, PlayersButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = player.Name;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => Bus.Publish(new TradePartnerChosenSignal(playerId)));
            }
        }

        public void OnDesiredUpdated(ResourceCostOrStock cardsDesired)
        {
            VisualsUI.ClearContainer(CardsReviewContainer);

            foreach (var (type, amount) in cardsDesired.ResourceDictionary)
            {
                for (int i = 0; i < amount; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.ReviewTrade, CardsReviewContainer);
                }
            }
        }
    }
}
