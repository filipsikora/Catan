using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterYearOfPlentyCard : BasePhaseAdapter
    {
        BinderCardSelection _binder;

        public AdapterYearOfPlentyCard(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            _binder = new BinderCardSelection(UI, EventBus, EventsHandler);
            _binder.Bind();

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Subscribe<SelectionChangedUIEvent>(OnDesiredCardsChanged);

            if (GameCache.GameFlow.PlayersToMove.Contains(GameCache.MyPlayer.PlayerId))
            {
                UI.CardSelectorPanel.Show("Choose two resources to get for free");
            }
        }

        private void OnDesiredCardsChanged(SelectionChangedUIEvent signal)
        {
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.ActionAvailable);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (signal.IsLeftClicked)
            {
                EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { isSelected = true, type = signal.Type });
            }

            else
            {
                EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { isSelected = false, type = signal.Type });
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(false);
            UI.CardSelectorPanel.gameObject.SetActive(false);
            UI.AwaitingPanel.Hide();

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Unsubscribe<SelectionChangedUIEvent>(OnDesiredCardsChanged);
        }
    }
}
