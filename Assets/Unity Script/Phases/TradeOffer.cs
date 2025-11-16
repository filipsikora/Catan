using Catan.Catan;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class TradeOffer : GamePhase
    {
        private ResourceCostOrStock _cardsOffered;

        private ResourceCostOrStock _cardsDesired = new ResourceCostOrStock();


        public TradeOffer(ResourceCostOrStock cardsOffered)
        {
            _cardsOffered = cardsOffered;
        }


        public override void OnEnter()
        {
            _cardsDesired = new ResourceCostOrStock();

            Manager.MainUIPanel.HideTradeOfferButton();
            Manager.TradeOfferPanel.Show(this);

            Manager.MainUIPanel.NextTurnButton.gameObject.SetActive(false);
            Manager.MainUIPanel.RollDiceButton.gameObject.SetActive(false);
            Manager.MainUIPanel.RolledNumberButton.gameObject.SetActive(false);

            Manager.TradeOfferPanel.OnCancelAction = OnCancelClicked;
            Manager.TradeOfferPanel.OnPlayerChosenAction = OnTradePartnerChosen;

            Manager.TradeOfferPanel.Bind(EnumTradeOfferUIButtons.CancelTradeOffer, OnCancelClicked);
        }

        public override void OnResourceCardClicked(VisualResourceCard card)
        {
            if (card.Container == Manager.TradeOfferPanel.CardsChoiceContainer)
            {
                EnumResourceTypes type = card.Type;
                _cardsDesired.ResourceDictionary[type] += 1;
                Manager.TradeOfferPanel.AddCardToReview(card);
            }

            if (card.Container == Manager.TradeOfferPanel.CardsReviewContainer)
            {
                EnumResourceTypes type = card.Type;
                _cardsDesired.ResourceDictionary[type] -= 1;
                Manager.TradeOfferPanel.RemoveCardFromReview(card);
            }

            if (_cardsDesired.ResourceDictionary.Values.Sum() > 0)
            {
                Manager.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(true);
            }

            else
            {
                Manager.TradeOfferPanel.gameObject.SetActive(false);
            }
        }

        public void OnTradePartnerChosen(Player player)
        {
            Manager.TradeOfferPanel.gameObject.SetActive(false);
            Manager.OnTradeRequested(player, _cardsOffered, _cardsDesired);
        }

        public void OnCancelClicked()
        {
            Manager.TradeOfferPanel.gameObject.SetActive(false);
            Manager.OnTradeFinished();
        }
    }
}