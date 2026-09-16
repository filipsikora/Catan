using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderNormalRound : BaseBinder
    {
        private readonly SelectionCache _selectionCache;
        public BinderNormalRound(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler, SelectionCache selectionCache) : base(ui, bus, eventsHandler)
        {
            _selectionCache = selectionCache;
        }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.OfferTrade, () =>
            {
                EventsHandler.Execute(EnumCommandType.OfferTradeCommand);
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BankTrade, () =>
            {
                EventsHandler.Execute(EnumCommandType.BankTradeCommand);
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildVillage, () =>
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

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuildRoad, () =>
            {
                Bus.Publish(new PositionsResetUIEvent());

                if (_selectionCache.SelectedEdgeId == null)
                {
                    Bus.Publish(new LogMessageUIEvent(EnumLogTypes.Info, "First select a road"));

                    return;
                }

                EventsHandler.Execute(EnumCommandType.BuildRoadCommand, new {edgeId = _selectionCache.SelectedEdgeId });

                _selectionCache.Clear();
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.UpgradeVillage, () =>
            {
                Bus.Publish(new PositionsResetUIEvent());

                if (_selectionCache.SelectedVertexId == null)
                {
                    Bus.Publish(new LogMessageUIEvent(EnumLogTypes.Info, "First select a vertex"));

                    return;
                }

                EventsHandler.Execute(EnumCommandType.UpgradeVillageCommand, new { vertexId = _selectionCache.SelectedVertexId });

                _selectionCache.Clear();
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
            {
                EventsHandler.Execute(EnumCommandType.ShowDevelopmentCardsCommand);
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.BuyDevelopmentCard, () =>
            {
                EventsHandler.Execute(EnumCommandType.BuyDevelopmentCardsCommand);
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.NextTurn, () =>
            {
                EventsHandler.Execute(EnumCommandType.EndTurnCommand);
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