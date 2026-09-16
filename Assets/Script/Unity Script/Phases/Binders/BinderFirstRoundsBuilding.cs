using Catan.Unity.Helpers;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Shared.Data;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Caches;

namespace Catan.Unity.Phases.Binders
{
    public class BinderFirstRoundBuildings : BaseBinder
    {
        private readonly SelectionCache _selectionCache;
        public BinderFirstRoundBuildings(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler, SelectionCache selectionCache) : base(ui, bus, eventsHandler)
        {
            _selectionCache = selectionCache;
        }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, () =>
            {
                Bus.Publish(new PositionsResetUIEvent());

                if (_selectionCache.SelectedVertexId == null)
                {
                    Bus.Publish(new LogMessageUIEvent(EnumLogTypes.Info, "First select a vertex"));

                    return;
                }

                EventsHandler.Execute(EnumCommandType.BuildVillageCommand, new { vertexId = _selectionCache.SelectedVertexId });

                _selectionCache.Clear();
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, () =>
            {
                Bus.Publish(new PositionsResetUIEvent());

                if (_selectionCache.SelectedEdgeId == null)
                {
                    Bus.Publish(new LogMessageUIEvent(EnumLogTypes.Info, "First select a road"));

                    return;
                }

                EventsHandler.Execute(EnumCommandType.BuildRoadCommand, new { edgeId = _selectionCache.SelectedEdgeId });

                _selectionCache.Clear();
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                EventsHandler.Execute(EnumCommandType.EndTurnCommand);
            });
        }

        public override void Unbind()
        {
            UI.MainUIPanel.Unbind(EnumMainUIButtons.BuildFreeVillage);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.BuildFreeRoad);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.NextTurn);
        }
    }
}