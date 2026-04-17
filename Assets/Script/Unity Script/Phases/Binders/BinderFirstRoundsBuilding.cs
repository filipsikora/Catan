using Catan.Unity.Helpers;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Shared.Data;

namespace Catan.Unity.Phases.Binders
{
    public class BinderFirstRoundBuildings : BaseBinder
    {
        public BinderFirstRoundBuildings(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, () =>
            {
                EventsHandler.Execute(EnumCommandType.BuildVillageCommand);
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, () =>
            {
                EventsHandler.Execute(EnumCommandType.BuildRoadCommand);
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