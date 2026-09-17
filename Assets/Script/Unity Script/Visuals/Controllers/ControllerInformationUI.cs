using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerInformationUI
    {
        private readonly InformationUI _informationUI;
        private readonly GameCache _gameCache;

        public ControllerInformationUI(InformationUI informationUI, EventBus bus, GameCache gameCache)
        {
            _informationUI = informationUI;
            _gameCache = gameCache;

            bus.Subscribe<TurnNumberChangedUIEvent>(OnTurnNumberChanged);
            bus.Subscribe<RolledNumberChangedUIEvent>(OnDiceRolled);
            bus.Subscribe<PlayerInformationTableChangedUIEvent>(OnInformationTableChanged);
        }

        private void OnTurnNumberChanged(TurnNumberChangedUIEvent signal)
        {
            _informationUI.UpdateTurnNumber(_gameCache.GameFlow.TurnNumber);
        }

        private void OnDiceRolled(RolledNumberChangedUIEvent signal)
        {
            _informationUI.UpdateRolledNumber(_gameCache.GameFlow.TurnNumber);
        } 

        private void OnInformationTableChanged(PlayerInformationTableChangedUIEvent signal)
        {
            var text = ""; // text made from cache
            _informationUI.UpdateInformationTable(text); 
        }
    }
}