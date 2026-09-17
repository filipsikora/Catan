using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterCardStealing : BasePhaseAdapter
    {
        public AdapterCardStealing(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            if (GameCache.GameFlow.PlayersToMove.Contains(GameCache.MyPlayer.PlayerId))
            {
                ShowVictimsCards();
            }

            else
            {
                UI.AwaitingPanel.Show();
            }
        }

        public void ShowVictimsCards()
        {
            _ = LoadData();
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(EnumCommandType.StolenCardSelectedCommand, new { type = signal.Type });
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<PlayerResourcesDto>(EnumQueryName.VictimCards);
            UI.CardTheftPanel.Show(snapshot);
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardTheftPanel.gameObject.SetActive(false);
            UI.AwaitingPanel.Hide();
        }
    }
}