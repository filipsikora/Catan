using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.PhaseLogic;
using Catan.Core.Results;
using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core
{
    public class GameSession
    {
        private readonly GameState _game;

        private readonly BankTradeLogic _bankTrade;
        private readonly BlockHexLogic _blockHex;
        private readonly SelectVictimLogic _selectVictim;
        private readonly BuildFreeRoadLogic _buildFreeRoad;
        private readonly BuildInitialRoadLogic _buildInitialRoad;

        public GameSession(GameState game)
        {
            _game = game;

            _bankTrade = new BankTradeLogic(this);
            _blockHex = new BlockHexLogic(this);
            _selectVictim = new SelectVictimLogic(this);
            _buildFreeRoad = new BuildFreeRoadLogic(this);
            _buildInitialRoad = new BuildInitialRoadLogic(this);
        }

        internal GameState Game => _game;

        // phase logic //

        public ResultBankTrade BankTrade(EnumResourceTypes offered, EnumResourceTypes desired) => _bankTrade.Handle(offered, desired);
        public ResultBlockHex BlockHex(int hexId) => _blockHex.Handle(hexId);
        public ResultCondition SelectVictim(int victimId) => _selectVictim.Handle(victimId);
        public ResultBuildFreeRoad BuildFreeRoad(int edgeId) => _buildFreeRoad.Handle(edgeId);
        public ResultBuildInitialRoad BuildInitialRoad(int edgeId, int vertexId) => _buildInitialRoad.Handle(edgeId, vertexId);

        // getters //

        public int GetCurrentPlayerId() => _game.CurrentPlayer.ID;

        public EnumGamePhases GetCurrentPhase() => _game.CurrentPhase;

        public int GetTradeRatioForCurrentPlayer(EnumResourceTypes resource)
        {
            if (_game.CurrentPlayer.Ports.Count != 0)
            {
                Port rightPort = _game.Map.PortList.Find(port => port.Type == resource);
                bool hasThreeToOnePort = _game.CurrentPlayer.Ports.Any(port => port.Type == null);

                if (_game.CurrentPlayer.Ports.Contains(rightPort))
                    return 2;

                if (hasThreeToOnePort)
                    return 3;
            }

            return 4;
        }

        public List<int> GetAdjacentToHexPlayersIds(int hexId)
        {
            var hex = _game.Map.GetHexById(hexId);

            List<int> adjacentPlayersIds = new();

            foreach (var vertex in hex.AdjacentVertices)
            {
                Player? owner = vertex.Owner;

                if (vertex.IsOwned && !adjacentPlayersIds.Contains(owner.ID))
                {

                    adjacentPlayersIds.Add(owner.ID);
                }
            }

            return adjacentPlayersIds;
        }

        public List<int> GetPossibleVictimsIds()
        {
            var blockedHexId = _game.BlockedHexId;

            if (blockedHexId == null)
                return new List<int>();

            var possibleVictimsIds = GetAdjacentToHexPlayersIds(blockedHexId.Value);
            possibleVictimsIds.Remove(GetCurrentPlayerId());

            return possibleVictimsIds;
        }

        public int GetDesertHexId()
        {
            var desertHex = _game.Map.HexList.Find(h => h.FieldType == EnumFieldTypes.Desert);

            return desertHex.Id;
        }

        public int GetCurrentPlayerResourceAmount(EnumResourceTypes resource)
        {
            return _game.CurrentPlayer.Resources.Get(resource);
        }

        // internal getters //

        internal Player GetCurrentPlayer() => _game.CurrentPlayer;
        internal Player GetPlayerById(int playerId) => _game.GetPlayerById(playerId);

        internal ResourceCostOrStock GetBank() => _game.Bank;

        internal HexTile GetHexById(int id) => _game.Map.GetHexById(id);
        internal Edge GetEdgeById(int id) => _game.Map.GetEdgeById(id);
        internal Vertex GetVertexById(int id) => _game.Map.GetVertexById(id);


        // internal setters //

        internal void BankTradeMutation(EnumResourceTypes offered, EnumResourceTypes desired, int ratio) => _game.BankTradeMutation(offered, desired, ratio);
        internal void BlockHexMutation(HexTile hex) => _game.BlockHexMutation(hex);
        internal void RoadBuiltMutation(Edge edge) => _game.RoadBuiltMutation(edge);
        internal void VillageBuiltMutation(Vertex vertex, bool secondVillage) => _game.VillageBuiltMutation(vertex, secondVillage);
        internal void RoadPaidAndBuiltMutation(Edge edge) => _game.RoadPaidAndBuiltMutation(edge);
        internal void VillagePaidAndBuiltMutation(Vertex vertex) => _game.VillagePaidAndBuiltMutation(vertex);

        internal void CreateCardsStealingContext(int victimId) => _game.CreateCardsStealingContext(victimId);
    }
}
