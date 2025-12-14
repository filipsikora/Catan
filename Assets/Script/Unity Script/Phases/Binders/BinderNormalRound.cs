    using Catan.Communication;
    using Catan.Communication.Signals;

    namespace Catan
    {
        public class BinderNormalRound : BaseBinder
        {
            public BinderNormalRound(ManagerUI ui, EventBus bus) : base(ui, bus) { }

            public override void Bind()
            {
                UI.MainUIPanel.Bind(EnumMainUIButtons.OfferTrade, () =>
                {
                    Bus.Publish(new RequestOfferTradeSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.BankTrade, () =>
                {
                    Bus.Publish(new RequestBankTradeSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.BuildVillage, () =>
                {
                    Bus.Publish(new RequestBuildVillageSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.BuildRoad, () =>
                {
                    Bus.Publish(new RequestBuildRoadSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.UpgradeVillage, () =>
                {
                    Bus.Publish(new RequestUpgradeVillageSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
                {
                    Bus.Publish(new RequestShowDevelopmentCardsSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.BuyDevelopmentCard, () =>
                {
                    Bus.Publish(new RequestBuyDevelopmentCardSignal());
                });

                UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
                {
                    Bus.Publish(new RequestEndTurnSignal());
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