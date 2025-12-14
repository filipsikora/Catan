using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class RobberPlacing : GamePhase
    {
        private bool _afterRoll;
        private HandlerRobberPlacing _handler;

        public RobberPlacing(bool afterRoll)
        {
            _afterRoll = afterRoll;
        }


        public override void OnEnter()
        {
            Debug.Log($"{Game.CurrentPlayer} chooses a hex to block");

            _handler = new HandlerRobberPlacing(Game, Manager.EventBus);

            Manager.EventBus.Subscribe<RobberPlacedSignal>(OnRobberPlaced);
            Manager.EventBus.Subscribe<VictimChosenSignal>(OnVictimChosen);
        }

        private void OnRobberPlaced(RobberPlacedSignal signal)
        {
            HexTile hex = Game.Map.HexList.Find(h => h.Id == signal.HexId);

            Manager.BoardVisuals.MoveRobberObject(hex);

            if (signal.VictimsIds.Count == 0)
            {
                Debug.Log("No victim to steal from");

                VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);

                Handler.TransitionTo(_afterRoll ? new NormalRound() : new BeforeRoll());
            }

            else
            {
                var victims = signal.VictimsIds.Select(id => Game.PlayerList[id]).ToList();

                VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
                UI.VictimSelectorPanel.Show(victims);
            }
        }

        private void OnVictimChosen(VictimChosenSignal signal)
        {
            Player victim = Game.PlayerList[signal.VictimId];

            Handler.TransitionTo(new CardStealing(victim, _afterRoll));
        }

        public override void OnExit()
        {
            _handler.Dispose();

            Manager.EventBus.Unsubscribe<RobberPlacedSignal>(OnRobberPlaced);
            Manager.EventBus.Unsubscribe<VictimChosenSignal>(OnVictimChosen);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}