#nullable enable
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Data;
using System.Collections.Generic;
using Catan.Unity.InternalUIEvents;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerResourceCards
    {
        private readonly Dictionary<int, VisualResourceCard> _cards = new();
        private readonly EventBus _bus;

        public ControllerResourceCards(EventBus bus)
        {
            _bus = bus;
            _bus.Subscribe<ResourceCardVisualStateChangedUIEvent>(OnResourceCardVisualStateChanged);
            _bus.Subscribe<MultipleResourceCardVisualStateResetUIEvent>(OnMultipleResourceCardsVisualStateChanged);
            _bus.Subscribe<ResourceCardTypeVisualStateChangedUIEvent>(OnResourceCardTypeVisualStateChanged);
            _bus.Subscribe<ResourceCardToggledUIEvent>(OnResourceCardToggled);
        }

        private void ApplyVisualChange(VisualResourceCard card, EnumResourceCardVisualState state)
        {
            switch (state)
            {
                case EnumResourceCardVisualState.None:
                    card.Reset();
                    break;

                case EnumResourceCardVisualState.Lifted:
                    card.MoveUp();
                    break;

                case EnumResourceCardVisualState.Highlighted:
                    card.Highlight();
                    break;
            }
        }

        private void OnResourceCardVisualStateChanged(ResourceCardVisualStateChangedUIEvent signal)
        {
            var card = GetVisualResourceCardById(signal.VisualResourceCardId);
            
            if (card == null)
                return;

            ApplyVisualChange(card, signal.State);
        }

        private void OnResourceCardTypeVisualStateChanged(ResourceCardTypeVisualStateChangedUIEvent signal)
        {
            foreach (var card in _cards.Values)
            {
                if (card.Type == signal.Type)
                {
                    ApplyVisualChange(card, signal.State);
                }
            }
        }

        private void OnMultipleResourceCardsVisualStateChanged(MultipleResourceCardVisualStateResetUIEvent signal)
        {
            foreach (var card in _cards.Values)
            {
                if (card.Location == signal.Location)
                {
                    card.Reset();
                }
            }
        }

        private void OnResourceCardToggled(ResourceCardToggledUIEvent signal)
        {
            var card = GetVisualResourceCardById(signal.VisualResourceCardId);

            ToggleCard(card);
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

        private void ToggleCard(VisualResourceCard card)
        {
            if (card == null)
                return;

            card.IsToggled = !card.IsToggled;
        }

        public void Dispose()
        {
            _bus.Unsubscribe<ResourceCardVisualStateChangedUIEvent>(OnResourceCardVisualStateChanged);
            _bus.Unsubscribe<MultipleResourceCardVisualStateResetUIEvent>(OnMultipleResourceCardsVisualStateChanged);
            _bus.Unsubscribe<ResourceCardTypeVisualStateChangedUIEvent>(OnResourceCardTypeVisualStateChanged);
            _bus.Unsubscribe<ResourceCardToggledUIEvent>(OnResourceCardToggled);
        }
    }
}