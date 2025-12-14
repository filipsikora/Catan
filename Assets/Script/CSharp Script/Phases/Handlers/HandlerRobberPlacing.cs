using Catan.Catan;
using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using System.Linq;
using UnityEngine;


namespace Catan.Core
{
    public class HandlerRobberPlacing : BaseHandler
    {
        private int? _hexId;

        public HandlerRobberPlacing(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<HexClickedSignal>(OnHexClicked);
        }

        private void OnHexClicked(HexClickedSignal signal)
        {
            _hexId = signal.HexId;
            HexTile hex = Game.Map.HexList.Find(h => h.Id == _hexId);

            if (hex.isBlocked)
            {
                Debug.Log("You have to move the robber.");
                return;
            }

            Game.BlockHex(hex);

            var victims = Game.GetPlayersAdjacentToHex(hex);
            victims.Remove(Game.CurrentPlayer);

            var victimIds = victims.Select(v => Game.PlayerList.IndexOf(v)).ToList();

            Bus.Publish(new RobberPlacedSignal(victimIds, hex.Id));
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<HexClickedSignal>(OnHexClicked);
        }
    }
}