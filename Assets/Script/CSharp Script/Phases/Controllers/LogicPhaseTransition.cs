using Catan.Core.Interfaces;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using UnityEngine;

namespace Catan.Core.Phases.Controllers
{
    public sealed class LogicPhaseTransition
    {
        private readonly EventBus _bus;
        public IPhaseLogic? Current { get; private set; }
        public LogicPhaseTransition(EventBus bus)
        {
            _bus = bus;
        }

        public void ChangePhase(IPhaseLogic next, EnumGamePhases phase)
        {
            Current?.Exit();
            Current = next;
            Current.Enter();

            _bus.Publish(new PhaseChangedEvent(phase));
        }
    }
}