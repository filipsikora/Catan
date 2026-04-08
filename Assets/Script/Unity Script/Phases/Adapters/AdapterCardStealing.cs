using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using System;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterCardStealing : BasePhaseAdapter
    {
        public AdapterCardStealing(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameClient client, Guid gameId) : base(ui, bus, eventHandler, client, gameId) { }

        public override void OnEnter()
        {
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            ShowVictimsCards();
        }

        public void ShowVictimsCards()
        {
            _ = LoadData();
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(EnumCommandType.StolenCardSelectedCommand, signal.Type);
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<PlayerCardsDto>(EnumQueryName.VictimCards);
            UI.CardTheftPanel.Show(snapshot);
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardTheftPanel.gameObject.SetActive(false);
        }
    }
}
