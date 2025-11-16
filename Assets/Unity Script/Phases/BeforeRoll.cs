using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class BeforeRoll : GamePhase
    {
        public override void OnEnter()
        {
            Manager.PlayerUIPanel.UpdatePlayerInfo(Game.currentPlayer);
            Manager.MainUIPanel.ShowRollDiceButton();
            Manager.MainUIPanel.HideTradeOfferButton();
        }

        public override void OnVertexClicked(Vertex vertex)
        {
            Debug.Log("Roll first");
        }

        public override void OnEdgeClicked(Edge edge)
        {
            Debug.Log("Roll first");
        }

        public override void OnHexClicked(HexTile hex)
        {
            Debug.Log("Roll first");
        }

        public override void OnRollDiceClicked()
        {
            Game.RollAndServePlayers();
            Manager.MainUIPanel.UpdateRolledDice(Game.LastRoll);
        }
    }
}
