using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Shared.Commands;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRoadBuilding : BasePhaseAdapter
    {
        private BinderNormalRound _binder;

        public AdapterRoadBuilding(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, false);
            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            EventBus.Subscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);
        }

        private void OnEdgeClicked(EdgeClickedUIEvent signal)
        {
            EventsHandler.Execute(new EdgeClickedCommand(signal.EdgeId));
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        public override void OnExit()
        {
            VisualsUI.SetParentVisibility(UI.PlayerUIPanel, true);

            EventBus.Unsubscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);
        }
    }
}