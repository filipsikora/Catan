using System;
using UnityEngine;

namespace Catan
{
    public class HandlerPhases
    {
        public GamePhase? CurrentPhase { get; private set; }

        public void TransitionTo(GamePhase newPhase)
        {
            CurrentPhase?.OnExit();

            CurrentPhase = newPhase;
            newPhase.Handler = this;

            CurrentPhase.OnEnter();
        }
    }
}