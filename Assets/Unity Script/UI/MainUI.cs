using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Catan
{
    public class MainUI : VisualButton<EnumMainUIButtons>
    {
        public TextMeshProUGUI TurnCounterText;
        public TextMeshProUGUI RolledNumberText;

        public Button RollDiceButton;
        public Button NextTurnButton;
        public Button BuildFreeVillageButton;
        public Button BuildFreeRoadButton;
        public Button BuildVillageButton;
        public Button BuildRoadButton;
        public Button UpgradeVillageButton;
        public Button RolledNumberButton;
        public Button OfferTradeButton;
        public Button BankTradeButton;
        public Button DevelopmentCardsButton;
        public Button BuyDevelopmentCardButton;

        private void Awake()
        {
            RegisterButton(EnumMainUIButtons.RollDice, RollDiceButton);
            RegisterButton(EnumMainUIButtons.NextTurn, NextTurnButton);
            RegisterButton(EnumMainUIButtons.BuildFreeVillage, BuildFreeVillageButton);
            RegisterButton(EnumMainUIButtons.BuildFreeRoad, BuildFreeRoadButton);
            RegisterButton(EnumMainUIButtons.BuildVillage, BuildVillageButton);
            RegisterButton(EnumMainUIButtons.BuildRoad, BuildRoadButton);
            RegisterButton(EnumMainUIButtons.UpgradeVillage, UpgradeVillageButton);
            RegisterButton(EnumMainUIButtons.RolledNumber, RolledNumberButton);
            RegisterButton(EnumMainUIButtons.OfferTrade, OfferTradeButton);
            RegisterButton(EnumMainUIButtons.BankTrade, BankTradeButton);
            RegisterButton(EnumMainUIButtons.DevelopmentCards, DevelopmentCardsButton);
            RegisterButton(EnumMainUIButtons.BuyDevelopmentCard, BuyDevelopmentCardButton);
        }

        public void UpdateTurnCounter(int turn)
        {
            TurnCounterText.text = $"Turn {turn}";
        }

        public void UpdateRolledDice(int lastRoll)
        {
            RolledNumberText.text = $"Last roll: {lastRoll}";
        }

        public void ShowRollDiceButton()
        {
            Hide(EnumMainUIButtons.NextTurn);
            Hide(EnumMainUIButtons.RolledNumber);
            Show(EnumMainUIButtons.RollDice);
            Hide(EnumMainUIButtons.BankTrade);
            Hide(EnumMainUIButtons.DevelopmentCards);
            Hide(EnumMainUIButtons.BuyDevelopmentCard);
        }

        public void ShowTradeOfferButton()
        {
            OfferTradeButton.gameObject.SetActive(true);
        }

        public void HideTradeOfferButton()
        {
            OfferTradeButton.gameObject.SetActive(false);
        }

        public void ShowNextTurnButton()
        {
            Show(EnumMainUIButtons.NextTurn);
            Show(EnumMainUIButtons.RolledNumber);
            Hide(EnumMainUIButtons.RollDice);
            Show(EnumMainUIButtons.BankTrade);
            Show(EnumMainUIButtons.DevelopmentCards);
            Show(EnumMainUIButtons.BuyDevelopmentCard);
        }

        public void HideStartingBuildings()
        {
            Hide(EnumMainUIButtons.BuildFreeRoad);
            Hide(EnumMainUIButtons.BuildFreeVillage);
        }

        public void HideLaterBuildings()
        {
            Hide(EnumMainUIButtons.BuildRoad);
            Hide(EnumMainUIButtons.BuildVillage);
            Hide(EnumMainUIButtons.UpgradeVillage);
        }
    }
}