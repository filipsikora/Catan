using Catan.Catan;
using UnityEngine;

namespace Catan
{
    public class CardStealing : GamePhase
    {
        private readonly Player _victim;

        public EnumResourceTypes cardStolen;

        public CardStealing(Player victim)
        {
            _victim = victim;
        }

        public override void OnEnter()
        {
            Manager.CardSelectorTheftPanel.Show(_victim, this);
        }

        public override void OnResourceCardClicked(VisualResourceCard card)
        {
            if (card.transform.parent == Manager.CardSelectorTheftPanel.CardsContainer)
            {
                Debug.Log($"{card} stolen");

                CurrentPlayer.Resources.AddSingleType(card.Type, 1);
                _victim.Resources.SubtractSingleType(card.Type, 1);

                Manager.CardSelectorTheftPanel.gameObject.SetActive(false);

                Manager.OnCardStolen();
            }
        }
    }
}
