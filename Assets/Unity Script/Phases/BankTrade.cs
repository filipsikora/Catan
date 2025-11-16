using Catan.Catan;
using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class BankTrade : GamePhase
    {
        public EnumResourceTypes? chosenResource = null;

        int ratio;

        public override void OnEnter()
        {
            Manager.BankTradePanel.Show();
            Manager.BankTradePanel.Bind(EnumBankTradeUIButtons.CancelBankTrade, OnBankTradeFinished);
        }

        public override void OnResourceCardClicked(VisualResourceCard card)
        {
            if (card.Container == Manager.BankTradePanel.OfferedCardsContainer)
            {
                ratio = Game.FindTradeRatio(card);
                bool possible = CurrentPlayer.Resources.ResourceDictionary[card.Type] >= ratio;
                chosenResource = card.Type;

                Manager.BankTradePanel.UpdateTradeRatio(ratio, possible, chosenResource);

            }

            if (card.Container == Manager.BankTradePanel.DesiredCardsContainer && chosenResource != null)
            {
                CurrentPlayer.Resources.SubtractSingleType(chosenResource.Value, ratio);
                Game.Bank.AddSingleType(chosenResource.Value, ratio);

                CurrentPlayer.Resources.AddSingleType(card.Type, 1);
                Game.Bank.SubtractSingleType(card.Type, 1);

                OnBankTradeFinished();
            }
        }

        public void OnBankTradeFinished()
        {
            Manager.BankTradePanel.gameObject.SetActive(false);
            Manager.OnTradeFinished();
        }
    }
}