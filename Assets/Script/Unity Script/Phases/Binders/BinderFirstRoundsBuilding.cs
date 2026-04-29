using Catan.Unity.Helpers;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Shared.Data;
using Catan.Unity.InternalUIEvents;

namespace Catan.Unity.Phases.Binders
{
    public class BinderFirstRoundBuildings : BaseBinder
    {
        public BinderFirstRoundBuildings(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, () =>
            {
                Bus.Publish(new PositionsResetUIEvent());

                if (EventsHandler.SelectedVertexId == null)
                {
                    Bus.Publish(new LogMessageUIEvent(EnumLogTypes.Info, "First select a vertex"));

                    return;
                }

                EventsHandler.Execute(EnumCommandType.BuildVillageCommand, new { vertexId = EventsHandler.SelectedVertexId });

                EventsHandler.ResetSelectedPositions();
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, () =>
            {
                Bus.Publish(new PositionsResetUIEvent());

                if (EventsHandler.SelectedEdgeId == null)
                {
                    Bus.Publish(new LogMessageUIEvent(EnumLogTypes.Info, "First select a road"));

                    return;
                }

                EventsHandler.Execute(EnumCommandType.BuildRoadCommand, new { edgeId = EventsHandler.SelectedEdgeId });

                EventsHandler.ResetSelectedPositions();
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