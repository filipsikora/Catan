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
        private readonly BuildInitialVillageLogic _buildInitialVillage;
        private readonly BuildRoadLogic _buildRoad;
        private readonly BuildVillageLogic _buildVillage;
        private readonly BuyDevCardLogic _buyDevCard;
        private readonly DiscardCardsLogic _discardCards;
        private readonly FinishTurnLogic _finishTurn;
        private readonly OfferTradeLogic _offerTrade;
        private readonly PlayDevCardLogic _playDevCard;
        private readonly ReactToTradeLogic _reactToTrade;
        private readonly RollDiceLogic _rollDice;
        private readonly StealCardLogic _stealCard;
        private readonly UpgradeVillageLogic _upgradeVillage;
        private readonly UseMonopolyLogic _useMonopoly;
        private readonly UseYearOfPlentyLogic _useYearOfPlenty;

        public GameSession(GameState game)
        {
            _game = game;

            _bankTrade = new BankTradeLogic(this);
            _blockHex = new BlockHexLogic(this);
            _selectVictim = new SelectVictimLogic(this);
            _buildFreeRoad = new BuildFreeRoadLogic(this);
            _buildInitialRoad = new BuildInitialRoadLogic(this);
            _buildInitialVillage = new BuildInitialVillageLogic(this);
            _buildRoad = new BuildRoadLogic(this);
            _buildVillage = new BuildVillageLogic(this);
            _buyDevCard = new BuyDevCardLogic(this);
            _discardCards = new DiscardCardsLogic(this);
            _finishTurn = new FinishTurnLogic(this);
            _offerTrade = new OfferTradeLogic(this);
            _playDevCard = new PlayDevCardLogic(this);
            _reactToTrade = new ReactToTradeLogic(this);
            _rollDice = new RollDiceLogic(this);
            _stealCard = new StealCardLogic(this);
            _upgradeVillage = new UpgradeVillageLogic(this);
            _useMonopoly = new UseMonopolyLogic(this);
            _useYearOfPlenty = new UseYearOfPlentyLogic(this);
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

        public bool GetAfterRoll() => _game.GetAfterRoll();

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

        public void GetPlayersToDiscard()
        {
            var playersToDiscard = _game.GetCardsDiscardingPlayers().Select(p => p.ID);

            CreateCardDiscardingContext(playersToDiscard);
        }

        public (int, bool) GetNextIndex()
        {
            bool initialRoundsRemaining;
            int nextIndex;

            if (_game.FirstRoundsIndices.Count > 0)
            {
                _game.FirstRoundsIndices.Dequeue();

                initialRoundsRemaining = true;
                nextIndex = initialRoundsRemaining ? _game.FirstRoundsIndices.Peek() : 0;

                return (nextIndex, initialRoundsRemaining);
            }

            initialRoundsRemaining = false;
            nextIndex = (_game.CurrentPlayerIndex + 1) % _game.PlayerList.Count;

            return (nextIndex, initialRoundsRemaining);
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

        internal DevelopmentCard GetFirstDevCard() => _game.DevelopmentCardsDeckAvailable[0];
        internal DevelopmentCard GetDevCardById(int cardId) => _game.GetDevCardById(cardId);

        internal HexTile GetHexById(int id) => _game.Map.GetHexById(id);
        internal Edge GetEdgeById(int id) => _game.Map.GetEdgeById(id);
        internal Vertex GetVertexById(int id) => _game.Map.GetVertexById(id);

        internal (bool exists, PlayerTradeContext context) TryGetPlayerTradeContext()
        {
            var context = _game.LastPlayerTradeOffered;

            return (context != null, context);
        }

        internal (bool exists, CardStealingContext context) TryGetCardStealingContext()
        {
            var context = _game.CardStealingProgress;

            return (context != null, context);
        }


        // internal setters //

        internal void BankTradeMutation(EnumResourceTypes offered, EnumResourceTypes desired, int ratio) => _game.BankTradeMutation(offered, desired, ratio);
        internal void BlockHexMutation(HexTile hex) => _game.BlockHexMutation(hex);
        internal Dictionary<int, int> UseMonopolyMutation(EnumResourceTypes resource) => _game.UseMonopolyMutation(resource);
        internal void UseYearOfPlentyMutation(ResourceCostOrStock resource) => _game.UseYearOfPlentyMutation(resource);
        internal void RoadBuiltMutation(Edge edge) => _game.RoadBuiltMutation(edge);
        internal void VillageBuiltMutation(Vertex vertex, bool secondVillage) => _game.VillageBuiltMutation(vertex, secondVillage);
        internal void TownPaidAndBuiltMutation(Vertex vertex) => _game.TownPaidAndBuiltMutation(vertex);
        internal void RoadPaidAndBuiltMutation(Edge edge) => _game.RoadPaidAndBuiltMutation(edge);
        internal void VillagePaidAndBuiltMutation(Vertex vertex) => _game.VillagePaidAndBuiltMutation(vertex);
        internal void BuyDevCardMutation(DevelopmentCard card) => _game.BuyDevCardMutation(card);
        internal void CardsDiscardedMutation(Player player, ResourceCostOrStock selected) => _game.CardsDiscardedMutation(player, selected);
        internal void CardsDiscardedContextMutation() => _game.CardsDiscardedContextMutation();
        internal void MarkDevCardsAsOldMutation() => _game.MarkDevCardsAsOldMutation();
        internal void AdvanceToNextPlayerMutation(int nextIndex) => _game.AdvanceToNextPlayerMutation(nextIndex);
        internal DevelopmentCard DevCardPlayedMutation(DevelopmentCard card) => _game.DevCardPlayedMutation(card);
        internal void PlayerTradeDoneMutation(Player seller, Player buyer, ResourceCostOrStock offered, ResourceCostOrStock desired) =>
    _game.PlayerTradeDoneMutation(seller, buyer, offered, desired);
        internal List<ResultDistributeResources> ServePlayersMutation() => _game.ServePlayersMutation();
        internal int DiceRolledMutation() => _game.DiceRolledMutation();
        internal void CardStolenMutation(Player victim, EnumResourceTypes resource) => _game.CardStolenMutation(victim, resource);

        internal void CreateCardsStealingContext(int victimId) => _game.CreateCardsStealingContext(victimId);
        internal void CreatePlayerTradeOfferedContext(int sellerId, int buyerId, string sellerName, string buyerName, ResourceCostOrStock offered, ResourceCostOrStock desired) =>
            _game.CreatePlayerTradeOfferedContext(sellerId, buyerId, sellerName, buyerName, offered, desired);
        internal void CreateCardDiscardingContext(IEnumerable<int> playersToDiscard) => _game.CreateCardDiscardingContext(playersToDiscard);

        // wrappers //

        internal void WinCheck() => _game.WinCheck();
        internal void RollDice() => _game.RollDice();
    }
}