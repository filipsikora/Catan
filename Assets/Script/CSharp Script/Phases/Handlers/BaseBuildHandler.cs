using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan.Core
{
    public abstract class BaseBuildHandler : BaseHandler
    {
        protected int? SelectedVertexId;
        protected int? SelectedEdgeId;

        protected BaseBuildHandler(GameState game, EventBus bus) : base(game, bus) { }

        protected void ResetSelection()
        {
            SelectedVertexId = null;
            SelectedEdgeId = null;
            Bus.Publish(new BuildOptionsShownSignal(false, false, false));
        }
    }
}