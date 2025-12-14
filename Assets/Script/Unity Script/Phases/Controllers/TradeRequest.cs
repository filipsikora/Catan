using Catan.Catan;
using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class TradeRequest : GamePhase
    {
        private HandlerTradeRequest _handler;
        private BinderTradeRequest _binder;

        private Player _player;
        private ResourceCostOrStock _cardsRequested;
        private ResourceCostOrStock _cardsOffered;

        public TradeRequest(Player player, ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsRequested)
        {
            _player = player;
            _cardsRequested = cardsRequested;
            _cardsOffered = cardsOffered;
        }

        public override void OnEnter()
        {
            _handler = new HandlerTradeRequest(Game, Manager.EventBus, _player, _cardsOffered, _cardsRequested);
            _binder = new BinderTradeRequest(UI, Manager.EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeRequestPanel.Show(_player, _cardsOffered, _cardsRequested);

            Manager.EventBus.Subscribe<TradeFinishedSignal>(OnTradeFinished);
            Manager.EventBus.Subscribe<TradeRequestSentSignal>(OnTradeRequestSent);
        }

        private void OnTradeRequestSent(TradeRequestSentSignal signal)
        {
            UI.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(signal.CanTrade);
        }

        private void OnTradeFinished(TradeFinishedSignal signal)
        {
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeRequestPanel.gameObject.SetActive(false);

            UI.UpdatePlayerInfo(Game.CurrentPlayer);
            UI.UpdatePlayerInfo(_player);

            Handler.TransitionTo(new NormalRound());
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<TradeFinishedSignal>(OnTradeFinished);
        }
    }
}
