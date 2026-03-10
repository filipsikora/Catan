using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderFirstRoundBuildings : BaseBinder
    {
        public BinderFirstRoundBuildings(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, () =>
            {
                Bus.Publish(new BuildVillageCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, () =>
            {
                Bus.Publish(new BuildRoadCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                Bus.Publish(new EndTurnCommand());
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