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
        }

        private void OnTurnNumberChanged(TurnNumberChangedUIEvent signal)
        {
            _mainUI.UpdateTurnCounter(signal.TurnNumber);
        }
    }
}