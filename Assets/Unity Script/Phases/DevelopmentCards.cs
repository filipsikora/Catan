using Catan.Catan;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Catan
{
    public class DevelopmentCards : GamePhase
    {
        public override void OnEnter()
        {
            Manager.DevelopmentCardsPanel.Show(CurrentPlayer);
            Manager.DevelopmentCardsPanel.Bind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards, OnDevelopmentCardsCanceled);
        }

        public override void OnDevelopmentCardClicked(VisualDevelopmentCard card)
        {
            CatanGameManager.Instance.DevelopmentCardHandler.UseCard(card.Type);
            ExitPhase();
        }

        public void OnDevelopmentCardsCanceled()
        {
            ExitPhase();
        }

        public void ExitPhase()
        {
            Manager.DevelopmentCardsPanel.gameObject.SetActive(false);
            Manager.PhaseHandler.TransitionTo(new NormalRound());
        }
    }
}
