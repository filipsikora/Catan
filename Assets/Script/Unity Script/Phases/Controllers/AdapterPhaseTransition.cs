using Catan.Unity.Phases.Adapters;
using UnityEngine;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterPhaseTransition
    {
        public BasePhaseAdapter? CurrentPhase { get; private set; }

        public void TransitionTo(BasePhaseAdapter newPhase)
        {
            Debug.Log($"[LOGIC] Entering phase: {newPhase.GetType().Name}");

            CurrentPhase?.OnExit();

            CurrentPhase = newPhase;
            newPhase.Handler = this;

            CurrentPhase.OnEnter();
        }
    }
}