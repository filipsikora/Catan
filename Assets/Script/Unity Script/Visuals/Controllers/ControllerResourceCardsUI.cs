using Catan.Communication;
using Catan.Communication.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Catan
{
    public class ControllerResourceCardsUI
    {
        private readonly Dictionary<int, VisualResourceCard> _cards = new();
        private readonly EventBus _bus;

        public ControllerResourceCardsUI(EventBus bus)
        {
            _bus = bus;
            _bus.Subscribe<ResourceCardVisualStateChangedSignal>(OnResourceCardVisualStateChanged);
            _bus.Subscribe<MultipleResourceCardVisualStateChangedResetSignal>(OnMultipleResourceCardsVisualStateChanged);
        }

        private void OnResourceCardVisualStateChanged(ResourceCardVisualStateChangedSignal signal)
        {
            var card = GetVisualResourceCardById(signal.VisualResourceCardId);

            if (card == null)
                return;

            switch (signal.State)
            {
                case EnumResourceCardVisualState.None:
                    VisualsUI.ResetResourceCard(card);
                    break;

                case EnumResourceCardVisualState.Lifted:
                    VisualsUI.MoveResourceCardUp(card);
                    break;

                case EnumResourceCardVisualState.Highlighted:
                    VisualsUI.HighlightResourceCard(card);
                    break;
            }
        }

        private void OnMultipleResourceCardsVisualStateChanged(MultipleResourceCardVisualStateChangedResetSignal signal)
        {
            foreach (var card in _cards.Values)
            {
                if (card.Location == signal.Location)
                {
                    VisualsUI.ResetResourceCard(card);
                }
            }
        }

        public void Register(VisualResourceCard card)
        {
            _cards[card.VisualResourceCardId] = card;
        }

        public void Unregister(VisualResourceCard card)
        {
            _cards.Remove(card.VisualResourceCardId);
        }

        public VisualResourceCard? GetVisualResourceCardById(int id)
        {
            _cards.TryGetValue(id, out var card);

            return card;
        }

        public void Dispose()
        {
            _bus.Unsubscribe<ResourceCardVisualStateChangedSignal>(OnResourceCardVisualStateChanged);
            _bus.Unsubscribe<MultipleResourceCardVisualStateChangedResetSignal>(OnMultipleResourceCardsVisualStateChanged);
        }
    }
}