using Catan.Catan;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Catan
{
    public class CardDiscarding : GamePhase
    {
        private Queue<Player> _playersToDiscard;

        private ResourceCostOrStock _selectedCards = new ResourceCostOrStock();

        private Player _currentPlayer;

        public CardDiscarding()
        {
            _playersToDiscard = new Queue<Player>(Game.PlayerList.Where(p => p.Resources.ResourceDictionary.Values.Sum() > 7));
        }

        public override void OnEnter()
        {
            Manager.MainUIPanel.Hide(EnumMainUIButtons.RollDice);
            Manager.MainUIPanel.Hide(EnumMainUIButtons.RolledNumber);
            ProceedToNextPlayer();
        }
        private void ResetSelection()
        {
            _selectedCards = new ResourceCostOrStock();
            Manager.CardSelectorRobberPanel.ConfirmDiscardButton.gameObject.SetActive(false);
        }

        public void ProceedToNextPlayer()
        {
            if (_playersToDiscard.Count == 0)
            {
                Manager.OnCardsDiscarded();
                return;
            }

            _currentPlayer = _playersToDiscard.Dequeue();

            ResetSelection();

            Manager.CardSelectorRobberPanel.Show(_currentPlayer, this);

            Manager.CardSelectorRobberPanel.Bind(EnumCardSelectorUIButtons.ConfirmDiscard, OnConfirmDiscard);
        }

        public override void OnResourceCardClicked(VisualResourceCard card)
        {
            if (card.transform.parent == Manager.CardSelectorRobberPanel.CardsContainer)
            {

                if (card.IsSelected)
                {
                    _selectedCards.SubtractSingleType(card.Type, 1);
                }

                else
                {
                    _selectedCards.AddSingleType(card.Type, 1);
                }
       
                UpdateResourceCardVisual(card);
                card.ToggleSelection();

                CheckIfEnoughSelected();
            }
        }

        public override void UpdateResourceCardVisual(VisualResourceCard card)
        {
            if (card.IsSelected)
            {
                VisualsUI.MoveResourceCardDown(card);
            }

            else
            {
                VisualsUI.MoveResourceCardUp(card);
            }
        }

        public void CheckIfEnoughSelected()
        {
            int cardsTotal = _currentPlayer.Resources.ResourceDictionary.Values.Sum();
            int requiredNumber = (int)Math.Ceiling(cardsTotal / 2.0);
            int cardsChosen = _selectedCards.ResourceDictionary.Values.Sum();

            if (cardsChosen == requiredNumber)
            {
                Manager.CardSelectorRobberPanel.ConfirmDiscardButton.gameObject.SetActive(true);
            }

            else
            {
                Manager.CardSelectorRobberPanel.ConfirmDiscardButton.gameObject.SetActive(false);
            }
        }

        public void OnConfirmDiscard()
        {
            _currentPlayer.Resources.SubtractCards(_selectedCards);
            Manager.Game.Bank.AddCards(_selectedCards);

            _selectedCards = new ResourceCostOrStock();

            Manager.CardSelectorRobberPanel.ConfirmDiscardButton.gameObject.SetActive(false);
            Manager.CardSelectorRobberPanel.gameObject.SetActive(false);

            Manager.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);

            ProceedToNextPlayer();
        }
    }
}