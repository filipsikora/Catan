using Catan.Catan;
using System;
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

        public ResourceCardFactory CardFactory;

        public Button CancelTradeButton;
        public GameObject PlayerButtonPrefab;

        public GamePhase CurrentPhase;

        public Action<Player> OnPlayerChosenAction;
        public Action OnCancelAction;


        public void Awake()
        {
            RegisterButton(EnumTradeOfferUIButtons.CancelTradeOffer, CancelTradeButton);
        }


        public void Show(GamePhase phase)
        {
            CurrentPhase = phase;

            gameObject.SetActive(true);

            VisualsUI.ClearContainer(CardsChoiceContainer);
            VisualsUI.ClearContainer(CardsReviewContainer);
            VisualsUI.ClearContainer(PlayersButtonsContainer);

            foreach (var key in CatanGameManager.Instance.Game.Bank.ResourceDictionary.Keys)
            {
                VisualsUI.DrawResourceCard(CardFactory, CardsChoiceContainer, key);
            }

            foreach (var player in CatanGameManager.Instance.Game.PlayerList)
            {

                if (player == CatanGameManager.Instance.Game.currentPlayer)
                {
                    continue;
                }

                var buttonObj = Instantiate(PlayerButtonPrefab, PlayersButtonsContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = player.Name;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => OnPlayerChosenAction?.Invoke(player));
            }
        }

        public void AddCardToReview(VisualResourceCard card)
        {
            var type = card.Type;
            VisualsUI.DrawResourceCard(CardFactory, CardsReviewContainer, type);
        }

        public void RemoveCardFromReview(VisualResourceCard card)
        {
            Destroy(card.gameObject);
        }
    }
}
