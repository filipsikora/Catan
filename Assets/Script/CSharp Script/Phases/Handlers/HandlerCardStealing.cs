using Catan.Catan;
using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using System.Linq;
using UnityEngine;


namespace Catan.Core
{
    public class HandlerCardStealing : BaseHandler
    {
        private Player _victim;

        public HandlerCardStealing(GameState game, EventBus bus, Player victim) : base(game, bus)
        {
            _victim = victim;

            Bus.Subscribe<ResourceCardClickedSignal>(OnCardClicked);
        }

        private void OnCardClicked(ResourceCardClickedSignal signal)
        {
            Game.CurrentPlayer.Resources.AddSingleType(signal.Card.LinkedCard.Type, 1);
            _victim.Resources.SubtractSingleType(signal.Card.LinkedCard.Type, 1);

            Bus.Publish(new StealingFinishedSignal());
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnCardClicked);
        }
    }
}