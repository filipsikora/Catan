using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerTurnVisuals
    {
        private readonly EventBus _bus;
        private readonly MainUI _mainUI;

        public ControllerTurnVisuals(EventBus bus, MainUI mainUI)
        {
            _bus = bus;
            _mainUI = mainUI;

            _bus.Subscribe<TurnNumberChangedUIEvent>(OnTurnNumberChanged);
            _bus.Subscribe<DiceRollChangedUIEvent>(OnDiceRolled);
        }

        private void OnTurnNumberChanged(TurnNumberChangedUIEvent signal)
        {
            _mainUI.UpdateTurnCounter(signal.TurnNumber);
        }

        private void OnDiceRolled(DiceRollChangedUIEvent signal)
        {
            _mainUI.UpdateRolledDice(signal.RolledNumber);
        }
    }
}