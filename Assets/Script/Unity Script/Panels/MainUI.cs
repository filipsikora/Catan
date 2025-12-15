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

        public Transform ButtonsContainer;

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

        public void ShowTradeOfferButton()
        {
            Show(EnumMainUIButtons.OfferTrade);
        }

        public void HideTradeOfferButton()
        {
            Hide(EnumMainUIButtons.OfferTrade);
        }
    }
}