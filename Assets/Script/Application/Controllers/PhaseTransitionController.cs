using Catan.Application.Phases;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Controllers
{
    public sealed class PhaseTransitionController
    {
        private readonly GameState _game;
        private readonly EventBus _bus;

        public BasePhase Current { get; set; }

        public PhaseTransitionController(GameState game, EventBus bus)
        {
            _game = game;
            _bus = bus;
        }

        public void ChangePhase(EnumGamePhases next)
        {
            Current = CreatePhase(next);

            _bus.Publish(new PhaseChangedEvent(next));
        }

        private BasePhase CreatePhase(EnumGamePhases next)
        {
            return next switch
            {
                EnumGamePhases.BeforeRoll => new BeforeRollPhase(_game, _bus, this),
                EnumGamePhases.FirstRoundsBuilding => new FirstRoundsBuildingPhase(_game, _bus, this),

            };
        }
    }
}
