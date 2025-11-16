using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class PlayerSetup : GamePhase
    {
        public override void OnEnter()
        {
            Manager.PlayerSelectorPanel.SetActive(true);
        }

        public void OnPlayerNumberSelected(int playerNumber)
        {
            Manager.Game = new GameState(new HexMap());
            Manager.Game.ReadyPlayer(playerNumber);
            Manager.Game.ReadyBoard();
            Manager.BuildMap();

            Manager.PlayerSelectorPanel.SetActive(false);
            Manager.StartGame();
            Manager.OnPlayersSetupCompleted();
        }

        public override void OnExit()
        {
        }
    }
}
