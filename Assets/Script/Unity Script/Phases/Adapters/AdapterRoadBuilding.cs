using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRoadBuilding : BasePhaseAdapter
    {
        private BinderNormalRound _binder;
        private SelectionCache _selectionCache;

        public AdapterRoadBuilding(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, SelectionCache selectionCache, GameCache gameCache) : base(ui, bus, eventHandler, gameCache)
        {
            _selectionCache = selectionCache;
        }

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus, EventsHandler, _selectionCache);
            _binder.Bind();

            EventBus.Subscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<PositionsResetUIEvent>(OnAllPositionsReset);
        }

        private void OnEdgeClicked(EdgeClickedUIEvent signal)
        {
            _selectionCache.SelectEdge(signal.EdgeId);

            EventsHandler.Execute(EnumCommandType.EdgeClickedCommand, new { edgeId = signal.EdgeId });
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnAllPositionsReset(PositionsResetUIEvent signal)
        {
            UI.MainUIPanel.BuildRoadButton.gameObject.SetActive(false);
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Unsubscribe<PositionsResetUIEvent>(OnAllPositionsReset);
        }
    }
}