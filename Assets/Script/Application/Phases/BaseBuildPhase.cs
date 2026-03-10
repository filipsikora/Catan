using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;

namespace Catan.Application.Phases
{
    public abstract class BaseBuildPhase : BasePhase
    {
        protected int? SelectedVertexId;
        protected int? SelectedEdgeId;

        protected BaseBuildPhase(Facade facade) : base(facade) { }

        protected IUIMessages ResetSelection()
        {
            SelectedVertexId = null;
            SelectedEdgeId = null;

            return new BuildOptionsSentMessage(false, false, false);
        }
    }
}