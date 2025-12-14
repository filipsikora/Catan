using Catan.Communication.Signals;
using UnityEngine;
using Catan.Communication;

namespace Catan
{
    public class ResourceCardUIHandler
    {
        private readonly EventBus _bus;

        public ResourceCardUIHandler(EventBus bus)
        {
            _bus = bus;
            _bus.Subscribe<ResourceCardSelectionChangedSignal>(OnCardSelectionChanged);
        }

        private void OnCardSelectionChanged(ResourceCardSelectionChangedSignal signal)
        {
            if (signal.IsSelected)
            {
                VisualsUI.MoveResourceCardDown(signal.Card);
            }

            else
            {
                VisualsUI.MoveResourceCardUp(signal.Card);
            }
        }

        public void Dispose()
        {
            _bus.Unsubscribe<ResourceCardSelectionChangedSignal>(OnCardSelectionChanged);
        }
    }
}