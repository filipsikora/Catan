using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class RobberPlacing : GamePhase
    {
        public override void OnEnter()
        {
            Manager.MainUIPanel.RollDiceButton.gameObject.SetActive(false);
            Manager.MainUIPanel.RolledNumberButton.gameObject.SetActive(true);

            Debug.Log($"7 rolled, {CurrentPlayer.PlayerColor} chooses where to place the robber.");
        }

        public override void OnHexClicked(HexTile hex)
        {
            if (Manager.VictimSelectorPanel.gameObject.activeSelf)
                return;

            Game.BlockHex(hex);

            Manager.BoardVisuals.MoveRobberObject(hex);

            var victims = Game.GetPlayersAdjacentToHex(hex);
            victims.Remove(CurrentPlayer);

            if (victims.Count == 0)
            {
                Manager.OnRobberPlaced(null);
                return;
            }

            Manager.VictimSelectorPanel.Show(victims, this);
        }

        public override void OnVictimChosen(Player victim)
        {
            Manager.OnRobberPlaced(victim);
        }
    }
}