    using Catan.Catan;
    using NUnit.Framework;
    using System.Linq;
    using UnityEngine;

    namespace Catan
    {
        public class TradeRequest : GamePhase
        {
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
                Manager.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(true);
                
                Manager.TradeRequestPanel.Show(_player, _cardsOffered, _cardsRequested);
                Manager.TradeRequestPanel.OnTradeAcceptedOrRefused = OnTradeDecision;

                Manager.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(true);
                Manager.TradeRequestPanel.RefuseTradeButton.gameObject.SetActive(true);

            if (!Conditions.CanAfford(_player.Resources, _cardsRequested))
            {
               Manager.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(false);
            }
        }

            public void OnTradeDecision(bool accepted)
            {
                if (accepted)
                {
                    CurrentPlayer.Resources.SubtractCards(_cardsOffered);
                    _player.Resources.AddCards(_cardsOffered);

                    CurrentPlayer.Resources.AddCards(_cardsRequested);
                    _player.Resources.SubtractCards(_cardsRequested);
                }

                Manager.TradeRequestPanel.gameObject.SetActive(false);
                Manager.OnTradeFinished();
            }
        }
    }
