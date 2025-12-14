using System;
using UnityEngine;

namespace Catan
{
    public class HandlerPhases
    {
        public GamePhase? CurrentPhase { get; private set; }

        public void TransitionTo(GamePhase newPhase)
        {
            Debug.Log($"PHASE TRANSITION: {CurrentPhase?.GetType().Name} → {newPhase.GetType().Name}");

            CurrentPhase?.OnExit();

            CurrentPhase = newPhase;
            newPhase.Handler = this;
            Debug.Log($"PHASE ENTER CALL: {newPhase.GetType().Name}.OnEnter()");


            CurrentPhase.OnEnter();
        }
    }
}