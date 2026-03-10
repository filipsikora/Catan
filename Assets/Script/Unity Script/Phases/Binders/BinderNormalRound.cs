using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderNormalRound : BaseBinder
    {
        public BinderNormalRound(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.OfferTrade, () =>
            {
                EventsHandler.Execute(new OfferTradeCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BankTrade, () =>
            {
                EventsHandler.Execute(new BankTradeCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildVillage, () =>
            {
                EventsHandler.Execute(new BuildVillageCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildRoad, () =>
            {
                EventsHandler.Execute(new BuildRoadCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.UpgradeVillage, () =>
            {
                EventsHandler.Execute(new UpgradeVillageCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
            {
                EventsHandler.Execute(new ShowDevelopmentCardsCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuyDevelopmentCard, () =>
            {
                EventsHandler.Execute(new BuyDevelopmentCardCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                EventsHandler.Execute(new EndTurnCommand());
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