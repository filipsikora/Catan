using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderFirstRoundBuildings : BaseBinder
    {
        public BinderFirstRoundBuildings(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, () =>
            {
                EventsHandler.Execute(new BuildVillageCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, () =>
            {
                EventsHandler.Execute(new BuildRoadCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                EventsHandler.Execute(new EndTurnCommand());
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