using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;

namespace Catan.Core.Phases.Handlers
{
    public abstract class BaseBuildPhaseLogic : BasePhaseLogic
    {
        protected int? SelectedVertexId;
        protected int? SelectedEdgeId;

        protected BaseBuildPhaseLogic(GameState game, EventBus bus) : base(game, bus) { }

        protected void ResetSelection()
        {
            SelectedVertexId = null;
            SelectedEdgeId = null;
            Bus.Publish(new BuildOptionsSentEvent(false, false, false));
        }
    }
}