using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterMonopolyCard : BasePhaseAdapter
    {
        public BinderCardSelection _binder;

        public override void OnEnter()
        {
            _binder = new BinderCardSelection(UI, EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.Show( "Choose resource to steal from the other players");

            _binder.Bind();

            EventBus.Subscribe<ResourceSelectedEvent>(OnResourceSelected);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }

        private void OnResourceSelected(ResourceSelectedEvent signal)
        {
            EventBus.Publish(new MultipleResourceCardVisualStateResetUICommand(Shared.Data.EnumResourceCardLocation.DesiredTrade));

            if (signal.Type != null)
            {
                EventBus.Publish(new ResourceCardTypeVisualStateChangedUICommand(signal.Type, Data.EnumResourceCardVisualState.Highlighted));
            }

            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.Selected);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventBus.Publish(new StolenCardSelectedCommand(signal.Type));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<ResourceSelectedEvent>(OnResourceSelected);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(false);
            UI.CardSelectorPanel.gameObject.SetActive(false);
            UI.PlayerUIPanel.UpdatePlayerInfo(ManagerGame.Instance.Game.GetCurrentPlayer());
        }
    }
}
