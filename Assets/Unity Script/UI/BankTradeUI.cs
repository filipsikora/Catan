using Catan.Catan;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class BankTradeUI : VisualButton<EnumBankTradeUIButtons>
    {
        public Transform OfferedCardsContainer;
        public Transform DesiredCardsContainer;

        public TextMeshProUGUI TextRatio;

        public ResourceCardFactory CardFactory;

        public Button CancelTrade;


        public void Show()
        {
            VisualsUI.ClearContainer(OfferedCardsContainer);
            VisualsUI.ClearContainer(DesiredCardsContainer);

            RegisterButton(EnumBankTradeUIButtons.CancelBankTrade, CancelTrade);

            gameObject.SetActive(true);

            foreach (var key in CatanGameManager.Instance.Game.Bank.ResourceDictionary.Keys)
            {
                VisualsUI.DrawResourceCard(CardFactory, OfferedCardsContainer, key);
                VisualsUI.DrawResourceCard(CardFactory, DesiredCardsContainer, key);
            }
        }

        public void UpdateTradeRatio(int ratio, bool possible, EnumResourceTypes? type)
        {
            string text;

            if (possible)
            {
                text = $"Trading {ratio} cards of {type} for...";
            }

            else if (!possible)
            {
                text = $"You don't have enough {type} to trade";
            }

            else
            {
                text = "Choose cards to trade with the bank";
            }

            TextRatio.text = text;
        }
    }
}
