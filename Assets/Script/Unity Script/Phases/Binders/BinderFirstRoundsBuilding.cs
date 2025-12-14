using Catan.Communication;
using Catan.Communication.Signals;
using UnityEngine;

namespace Catan
{
    public class BinderFirstRoundBuildings : BaseBinder
    {
        public BinderFirstRoundBuildings(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, () =>
            {
                Bus.Publish(new RequestBuildVillageSignal());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, () =>
            {
                Bus.Publish(new RequestBuildRoadSignal());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                Bus.Publish(new RequestEndTurnSignal());
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