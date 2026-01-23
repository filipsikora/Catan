using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Application.Controllers;

namespace Catan.Application.Phases
{
    public abstract class BaseBuildPhase : BasePhase
    {
        protected int? SelectedVertexId;
        protected int? SelectedEdgeId;

        protected BaseBuildPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        protected void ResetSelection()
        {
            SelectedVertexId = null;
            SelectedEdgeId = null;

            Bus.Publish(new BuildOptionsSentEvent(false, false, false));
        }
    }
}