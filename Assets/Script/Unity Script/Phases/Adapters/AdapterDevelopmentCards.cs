using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterDevelopmentCards : BasePhaseAdapter
    {
        private BinderDevelopmentCards _binder;

        public AdapterDevelopmentCards(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            UI.DevelopmentCardsPanel.gameObject.SetActive(true);

            _binder = new BinderDevelopmentCards(UI, EventBus, EventsHandler);
            _binder.Bind();


            EventBus.Subscribe<DevelopmentCardClickedUIEvent>(OnDevCardClicked);

            if (GameCache.GameFlow.PlayersToMove.Contains(GameCache.MyPlayer.PlayerId))
            {
                UI.DevelopmentCardsPanel.Show(GameCache.MyPlayer.DevCards, EventBus);
            }
        }

        private void OnDevCardClicked(DevelopmentCardClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.DevelopmentCardClickedPlayedCommand, new { developmentCardId = signal.CardId });
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.DevelopmentCardsPanel.gameObject.SetActive(false);

            EventBus.Unsubscribe<DevelopmentCardClickedUIEvent>(OnDevCardClicked);
        }
    }
}