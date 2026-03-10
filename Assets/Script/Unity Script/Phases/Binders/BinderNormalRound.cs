using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderNormalRound : BaseBinder
    {
        public BinderNormalRound(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.OfferTrade, () =>
            {
                Bus.Publish(new OfferTradeCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BankTrade, () =>
            {
                Bus.Publish(new BankTradeCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildVillage, () =>
            {
                Bus.Publish(new BuildVillageCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildRoad, () =>
            {
                Bus.Publish(new BuildRoadCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.UpgradeVillage, () =>
            {
                Bus.Publish(new UpgradeVillageCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
            {
                Bus.Publish(new ShowDevelopmentCardsCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuyDevelopmentCard, () =>
            {
                Bus.Publish(new BuyDevelopmentCardCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                Bus.Publish(new EndTurnCommand());
            });
        }

        public override void Unbind()
        {
            UI.MainUIPanel.Unbind(EnumMainUIButtons.OfferTrade);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.BankTrade);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.BuildVillage);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.BuildRoad);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.UpgradeVillage);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.DevelopmentCards);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.BuyDevelopmentCard);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.NextTurn);
        }
    }
}