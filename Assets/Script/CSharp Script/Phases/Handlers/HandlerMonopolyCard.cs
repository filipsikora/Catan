using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;


namespace Catan.Core
{
    public class HandlerMonopolyCard : BaseHandler
    {
        private EnumResourceTypes? _type;

        public HandlerMonopolyCard(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<ResourceCardClickedSignal>(OnResourceCardClicked);

            Bus.Subscribe<CardSelectionAcceptedSignal>(OnResourceAccepted);
        }

        private void OnResourceCardClicked(ResourceCardClickedSignal signal)
        {
            EnumResourceTypes type = signal.Card.LinkedCard.Type;

            if (!signal.IsLeftClick)
                return;

            if (_type == type)
            {
                _type = null;
            }
            else
            {
                _type = type;
            }

            bool hasSelected = _type != null;
            Bus.Publish(new ResourceSelectedSignal(hasSelected));
        }

        private void OnResourceAccepted(CardSelectionAcceptedSignal signal)
        {
            if (_type == null)
                return;

            Game.OnMonopolyUsed(Game.CurrentPlayer, _type.Value);

            Bus.Publish(new MonopolyCardFinishedSignal());
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnResourceCardClicked);

            Bus.Unsubscribe<CardSelectionAcceptedSignal>(OnResourceAccepted);
        }
    }
}