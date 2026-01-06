using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterDevelopmentCards : BasePhaseAdapter
    {
        private BinderDevelopmentCards _binder;

        public override void OnEnter()
        {
            UI.DevelopmentCardsPanel.gameObject.SetActive(true);

            _binder = new BinderDevelopmentCards(UI, Manager.EventBus);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            var currentPlayerDevCardsSnapshots = Manager.DevCardsQueryService.GetCurrentPlayerDevCards();

            UI.DevelopmentCardsPanel.Show(currentPlayerDevCardsSnapshots);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.DevelopmentCardsPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
        }
    }
}