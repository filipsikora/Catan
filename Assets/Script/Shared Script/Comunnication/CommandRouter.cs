using Catan.Shared.Interfaces;
using Catan.Core.Phases.Controllers;
using UnityEngine;

namespace Catan.Shared.Communication
{
    public sealed class CommandRouter
    {
        private readonly LogicPhaseTransition _phases;

        public CommandRouter(EventBus bus, LogicPhaseTransition phases)
        {
            _phases = phases;
            bus.Subscribe<ICommand>(Dispatch);
        }

        private void Dispatch(object command)
        {
            Debug.Log($"[ROUTER] {command.GetType().Name} -> {_phases.Current?.GetType().Name}");

            _phases.Current?.Handle(command);
        }
    }
}