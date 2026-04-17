using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterDevelopmentCards : BasePhaseAdapter
    {
        private BinderDevelopmentCards _binder;

        public AdapterDevelopmentCards(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

        public override void OnEnter()
        {
            UI.DevelopmentCardsPanel.gameObject.SetActive(true);

            _binder = new BinderDevelopmentCards(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<DevelopmentCardClickedUIEvent>(OnDevCardClicked);

            _ = LoadData();
        }

        private void OnDevCardClicked(DevelopmentCardClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.DevelopmentCardClickedPlayedCommand, new { cardId = signal.CardId });
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<List<DevelopmentCardDto>>(EnumQueryName.CurrentPlayerDevCards);
            UI.DevelopmentCardsPanel.Show(snapshot);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.DevelopmentCardsPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Unsubscribe<DevelopmentCardClickedUIEvent>(OnDevCardClicked);
        }
    }
}