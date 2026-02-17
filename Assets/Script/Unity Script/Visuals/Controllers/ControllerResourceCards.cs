using Catan.Shared.Communication;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Data;
using System.Collections.Generic;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerResourceCards
    {
        private readonly Dictionary<int, VisualResourceCard> _cards = new();
        private readonly EventBus _bus;

        public ControllerResourceCards(EventBus bus)
        {
            _bus = bus;
            _bus.Subscribe<ResourceCardVisualStateChangedUICommand>(OnResourceCardVisualStateChanged);
            _bus.Subscribe<MultipleResourceCardVisualStateResetUICommand>(OnMultipleResourceCardsVisualStateChanged);
            _bus.Subscribe<ResourceCardTypeVisualStateChangedUICommand>(OnResourceCardTypeVisualStateChanged);
            _bus.Subscribe<ResourceCardToggledUICommand>(OnResourceCardToggled);
        }

        private void ApplyVisualChange(VisualResourceCard card, EnumResourceCardVisualState state)
        {
            switch (state)
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

        private void OnResourceCardVisualStateChanged(ResourceCardVisualStateChangedUICommand signal)
        {
            var card = GetVisualResourceCardById(signal.VisualResourceCardId);
            
            if (card == null)
                return;

            ApplyVisualChange(card, signal.State);
        }

        private void OnResourceCardTypeVisualStateChanged(ResourceCardTypeVisualStateChangedUICommand signal)
        {
            foreach (var card in _cards.Values)
            {
                if (card.Type == signal.Type)
                {
                    ApplyVisualChange(card, signal.State);
                }
            }
        }

        private void OnMultipleResourceCardsVisualStateChanged(MultipleResourceCardVisualStateResetUICommand signal)
        {
            foreach (var card in _cards.Values)
            {
                if (card.Location == signal.Location)
                {
                    VisualsUI.ResetResourceCard(card);
                }
            }
        }

        private void OnResourceCardToggled(ResourceCardToggledUICommand signal)
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
            _bus.Unsubscribe<ResourceCardVisualStateChangedUICommand>(OnResourceCardVisualStateChanged);
            _bus.Unsubscribe<MultipleResourceCardVisualStateResetUICommand>(OnMultipleResourceCardsVisualStateChanged);
            _bus.Unsubscribe<ResourceCardTypeVisualStateChangedUICommand>(OnResourceCardTypeVisualStateChanged);
            _bus.Unsubscribe<ResourceCardToggledUICommand>(OnResourceCardToggled);
        }
    }
}