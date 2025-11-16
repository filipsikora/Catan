using UnityEngine;

namespace Catan
{
    public class PhaseHandler
    {
        public GamePhase CurrentPhase { get; private set; }

        public void TransitionTo(GamePhase newPhase)
        {
            if (CurrentPhase != null)
            {
                CurrentPhase.VictimChosen -= CatanGameManager.Instance.OnVictimChosen;
                CurrentPhase.OnExit();
            }
            CurrentPhase = newPhase;

            CurrentPhase.VictimChosen += CatanGameManager.Instance.OnVictimChosen;

            CurrentPhase.OnEnter();
        }
    }
}