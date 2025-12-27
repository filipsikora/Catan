#nullable enable
using Catan.Core.Helpers;
using Catan.Core.Interfaces;
using Catan.Core.Models;
using Vertex = Catan.Core.Models.Vertex;
using Edge = Catan.Core.Models.Edge;
using Catan.Shared.Data;
using Catan.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Engine
{
    public class GameState
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public ResourceCostOrStock Bank { get; set; } = new ResourceCostOrStock(19, 19, 19, 10, 19);

        public List<DevelopmentCard> DevelopmentCardsDeckAvailable { get; private set; } = new();
        public List<DevelopmentCard> DevelopmentCardsDeckAll { get; private set; } = new();

        public bool AnyoneHasTenPoints { get; set; } = false;

        public Queue<int> FirstRoundsIndices { get; set; } = new();

        public System.Random Random { get; } = new System.Random();

        public int LastRoll { get; set; } = 0;
        public bool AfterRoll { get; set; }

        public int Turn { get; set; } = 1;

        public HexMap? Map { get; set; } = null;

        public List<int> FieldNumbersList { get; set; } = new List<int> { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11, 12 };

        public Player? CurrentPlayer = null;

        public int CurrentPlayerIndex = 0;

        public Vertex? LastPlacedVillagePosition = null;

        public int MostKnightsUsed = 0;

        public int LongestRoad = 0;

        public Player? KnightChampion = null;

        public Player? RoadChampion = null;

        public int RequiredRoads = 5;
        public int RequiredKnights = 3;
        public int PointsReward = 2;
        public int RequiredPoints = 10;

        public Dictionary<EnumFieldTypes, int> FieldTypesAmount { get; set; } = new Dictionary<EnumFieldTypes, int>
            {
                { EnumFieldTypes.Wheat, 4 },
                { EnumFieldTypes.Wood, 4 },
                { EnumFieldTypes.Wool, 4 },
                { EnumFieldTypes.Stone, 3 },
                { EnumFieldTypes.Clay, 3 },
                { EnumFieldTypes.Desert, 1 }
            };

        public List<EnumFieldTypes> FieldTypesList { get; set; } = new List<EnumFieldTypes>();

        public GameState(HexMap map)
        {
            Map = map;
        }

        public void ReadyFieldList()
        {
            FieldTypesList.Clear();

            foreach (var entry in FieldTypesAmount)
            {
                int i = FieldTypesAmount[entry.Key];
                {
                    for (int value = 1; value <= i; value++)
                    {
                        FieldTypesList.Add(entry.Key);
                    }
                }
            }

            FieldTypesList = FieldTypesList.OrderBy(_ => Random.Next()).ToList();
        }

        public void GiveHexesData(List<HexTile> hexList)
        {
            int numberIndex = 0;
            for (int index = 0; index < hexList.Count; index++)
            {
                hexList[index].FieldType = FieldTypesList[index];

                if (hexList[index].FieldType.Value != EnumFieldTypes.Desert)
                {
                    hexList[index].FieldNumber = FieldNumbersList[numberIndex];
                    numberIndex++;
                }
            }
        }

        public void RollDice()
        {
            int diceOne = Random.Next(1, 7);
            int diceTwo = Random.Next(1, 7);

            LastRoll = diceOne + diceTwo;
        }

        public ResultDiceRoll RollAndServePlayers()
        {
            var resultList = new List<ResultResourceDistribution>();

            RollDice();

            if (Map == null)
                return new ResultDiceRoll(LastRoll, resultList);

            foreach (HexTile hex in Map.HexList)
            {
                if (hex.FieldType == EnumFieldTypes.Desert)
                    continue;

                if (hex.isBlocked)
                    continue;

                if (hex.FieldNumber == LastRoll)
                {
                    foreach (Vertex vertex in hex.AdjacentVertices)
                    {
                        if (!vertex.IsOwned)
                            continue;

                        else
                        {
                            int requested = vertex.HasVillage ? 1 : 2;
                            Player owner = vertex.Owner;
                            EnumResourceTypes type = hex.GetResourceType().GetValueOrDefault();

                            int granted = Bank.SubtractUpTo(type, requested);
                            owner.Resources.AddExactAmount(type, granted);

                            var result = new ResultResourceDistribution(owner.ID, type, requested, granted);
                            resultList.Add(result);
                        }
                    }
                }
            }

            return new ResultDiceRoll(LastRoll, resultList);
        }

        public void ReadyPlayer(int playerNumber)
        {
            for (int i = 1; i <= playerNumber; i++)
            {
                string name = $"Player{i}";
                Player player = new Player(name, i);

                PlayerList.Add(player);

                foreach (var key in player.Resources.ResourceDictionary.Keys.ToList())
                {
                    player.Resources.ResourceDictionary[key] = 4;
                }
            }

            FirstRoundsIndices = SetupFirstRoundsIndices(playerNumber);

            PlayerList = PlayerList.OrderBy(_ => Random.Next()).ToList();
            CurrentPlayerIndex = FirstRoundsIndices.ElementAt(0);
            CurrentPlayer = PlayerList[CurrentPlayerIndex];
        }

        public Queue<int> SetupFirstRoundsIndices(int playerNumber)
        {
            FirstRoundsIndices.Clear();

            for (int i = 0; i < playerNumber; i++)
                FirstRoundsIndices.Enqueue(i);

            for (int i = playerNumber - 1; i >= 0; i--)
                FirstRoundsIndices.Enqueue(i);

            return FirstRoundsIndices;
        }

        public List<Player> GetPlayersAdjacentToHex(HexTile hex)
        {
            List<Player> adjacentPlayers = new();

            foreach (var vertex in hex.AdjacentVertices)
            {
                Player? owner = vertex.Owner;

                if (vertex.IsOwned && !adjacentPlayers.Contains(owner))
                {
                    adjacentPlayers.Add(owner);
                }
            }

            return adjacentPlayers;
        }

        public void BlockHex(HexTile hex)
        {
            HexTile previouslyBlocked = Map.HexList.Find(h => h.isBlocked);

            if (previouslyBlocked != null)
            {
                previouslyBlocked.isBlocked = false;
            }

            hex.isBlocked = true;
        }

        public void ReadyBoard()
        {
            ReadyFieldList();

            if (Map != null)
            {
                GiveHexesData(Map.HexList);
            }
        }

        public void PayCost<T>(Player player)
            where T : Building, IBuildingData
        {
            var cost = BuildingDataRegistry.Cost[typeof(T)];

            foreach (var resource in cost.ResourceDictionary.Keys)
            {
                player.Resources.ResourceDictionary[resource] -= cost.ResourceDictionary[resource];
                Bank.ResourceDictionary[resource] += cost.ResourceDictionary[resource];
            }
        }

        public void EndTurn()
        {
            WinCheck();
            Turn++;

            if (FirstRoundsIndices.Count > 0)
            {
                FirstRoundsIndices.Dequeue();

                if (FirstRoundsIndices.Count > 0)
                {
                    CurrentPlayerIndex = FirstRoundsIndices.Peek();
                }

                else
                {
                    CurrentPlayerIndex = 0;
                }
            }

            else
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerList.Count;
            }

            CurrentPlayer = PlayerList[CurrentPlayerIndex];
        }

        public Result<BuildingVillage> BuildFreeVillage(Player player, Vertex vertex)
        {
            ResultCondition result;

            result = Conditions.PositionExists(vertex.Id, id => Map.GetVertexById(id));
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.IsNotOwned(vertex);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.NoSettlementsInRange(vertex);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            var village = new BuildingVillage(player, vertex.X, vertex.Y, vertex);
            player.Buildings.Add(village);

            vertex.HasVillage = true;
            vertex.Owner = player;

            LastPlacedVillagePosition = vertex;

            if (vertex.HasPort)
            {
                player.Ports.Add(vertex.Port);
            }

            player.CountPoints();

            return Result<BuildingVillage>.Ok(village);
        }

        public Result<BuildingRoad> BuildFreeRoad(Player player, Edge edge)
        {
            ResultCondition result;

            result = Conditions.PositionExists(edge.Id, id => Map.GetVertexById(id));
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            result = Conditions.IsNotOwned(edge);
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            result = Conditions.HasAccessToPosition(player, edge);
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            var road = new BuildingRoad(player, edge.X, edge.Y, edge);
            player.Buildings.Add(road);

            edge.Owner = player;

            player.CountPoints();

            return Result<BuildingRoad>.Ok(road);
        }

        public Result<BuildingVillage> BuildVillage(Player player, Vertex vertex)
        {
            ResultCondition result;

            result = Conditions.PositionExists(vertex.Id, id => Map.GetVertexById(id));
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.HasAccessToPosition(player, vertex);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.IsNotOwned(vertex);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.NoSettlementsInRange(vertex);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.HasAvailable<BuildingVillage>(player);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            result = Conditions.CanAfford(player.Resources, BuildingDataRegistry.Cost[typeof(BuildingVillage)]);
            if (!result.Success)
                return Result<BuildingVillage>.Fail(result.Reason);

            PayCost<BuildingVillage>(player);

            var village = new BuildingVillage(player, vertex.X, vertex.Y, vertex);
            player.Buildings.Add(village);

            vertex.HasVillage = true;
            vertex.Owner = player;

            if (vertex.HasPort)
            {
                player.Ports.Add(vertex.Port);
            }

            player.CountPoints();

            UpdateRoadChampion();

            return Result<BuildingVillage>.Ok(village);
        }

        public Result<BuildingRoad> BuildRoad(Player player, Edge edge)
        {
            ResultCondition result;

            result = Conditions.PositionExists(edge.Id, id => Map.GetVertexById(id));
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            result = Conditions.HasAccessToPosition(player, edge);
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            result = Conditions.IsNotOwned(edge);
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            result = Conditions.HasAvailable<BuildingVillage>(player);
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            result = Conditions.CanAfford(player.Resources, BuildingDataRegistry.Cost[typeof(BuildingRoad)]);
            if (!result.Success)
                return Result<BuildingRoad>.Fail(result.Reason);

            PayCost<BuildingRoad>(player);

            var road = new BuildingRoad(player, edge.X, edge.Y, edge);
            player.Buildings.Add(road);

            edge.Owner = player;

            UpdateRoadChampion();

            return Result<BuildingRoad>.Ok(road);
        }

        public void CheckChampionship(Player player, ref int currentMax, int playerMax, int required, ref Player? currentChampion)
        {
            if (playerMax >= required && playerMax > currentMax)
            {
                currentMax = playerMax;

                if (currentChampion != null)
                {
                    currentChampion.ExtraPoints -= 2;
                    currentChampion.CountPoints();
                }

                player.ExtraPoints += 2;
                currentChampion = player;
                player.CountPoints();
            }
        }

        private int RecomputePlayerLongestRoad(Player player)
        {
            var calculator = new QuikGraphLongestRoad(Map.VertexList, Map.Edges, player);
            int length = calculator.ComputeLongestRoad();
            player.LongestRoadCount = length;

            return length;
        }

        public void UpdateRoadChampion()
        {
            foreach (var player in PlayerList)
            {
                RecomputePlayerLongestRoad(player);
            }

            var eligible = PlayerList.Where(p => p.LongestRoadCount >= RequiredRoads).ToList();

            if (eligible.Count == 0)
            {
                if (RoadChampion != null)
                {
                    RoadChampion.ExtraPoints -= PointsReward;
                    RoadChampion.CountPoints();
                    RoadChampion = null;
                    LongestRoad = 0;
                }

                return;
            }

            int maxLength = eligible.Max(p => p.LongestRoadCount);
            var candidates = eligible.Where(p => p.LongestRoadCount == maxLength).ToList();

            if (RoadChampion != null && candidates.Contains(RoadChampion))
                return;

            var newChampion = candidates[0];
            if (RoadChampion != null)
            {
                RoadChampion.ExtraPoints -= PointsReward;
                RoadChampion.CountPoints();
            }

            newChampion.ExtraPoints += PointsReward;
            newChampion.CountPoints();
            RoadChampion = newChampion;
            LongestRoad = maxLength;
        }

        public Result<BuildingTown> UpgradeVillage(Player player, Vertex vertex)
        {
            ResultCondition result;

            result = Conditions.PositionExists(vertex.Id, id => Map.GetVertexById(id));
            if (!result.Success)
                return Result<BuildingTown>.Fail(result.Reason);

            result = Conditions.HasAvailable<BuildingVillage>(player);
            if (!result.Success)
                return Result<BuildingTown>.Fail(result.Reason);

            result = Conditions.CanAfford(player.Resources, BuildingDataRegistry.Cost[typeof(BuildingTown)]);
            if (!result.Success)
                return Result<BuildingTown>.Fail(result.Reason);

            result = Conditions.HasVillage(player, vertex);
            if (!result.Success)
                return Result<BuildingTown>.Fail(result.Reason);

            PayCost<BuildingTown>(player);

            var town = new BuildingTown(player, vertex.X, vertex.Y, vertex);
            var village = player.Buildings.FirstOrDefault(b => b is BuildingVillage v && v.Vertex == vertex);
            player.Buildings.Remove(village);
            player.Buildings.Add(town);

            vertex.HasVillage = false;
            vertex.HasTown = true;

            player.CountPoints();

            return Result<BuildingTown>.Ok(town);
        }

        public int FindTradeRatio(EnumResourceTypes type)
        {
            if (CurrentPlayer.Ports.Count != 0)
            {
                Port rightPort = Map.PortList.Find(port => port.Type == type);
                bool hasThreeToOnePort = CurrentPlayer.Ports.Any(port => port.Type == null);

                if (CurrentPlayer.Ports.Contains(rightPort))
                    return 2;

                if (hasThreeToOnePort)
                    return 3;
            }

            return 4;
        }

        public (bool village, bool road, bool town) CheckBuildOptions(IPositionData position)
        {
            if (position is Vertex)
            {
                return (true, false, true);
            }

            else
            {
                return (false, true, false);
            }
        }

        public void PrepareDevelopmentDeck()
        {
            DevelopmentCardsDeckAvailable.Clear();
            int id = 1;

            foreach (var entry in DevelopmentCardDataRegistry.TotalCount)
            {
                var type = entry.Key;
                var amount = entry.Value;

                for (int i = 0; i < amount; i++)
                {
                    DevelopmentCard card = new DevelopmentCard(type, id);
                    DevelopmentCardsDeckAvailable.Add(card);
                    DevelopmentCardsDeckAll.Add(card);
                    id++;
                }
            }

            DevelopmentCardsDeckAvailable = DevelopmentCardsDeckAvailable.OrderBy(_ => Random.Next()).ToList();
        }

        public Result<DevelopmentCard> BuyDevelopmentCard(Player player)
        {
            var cost = new ResourceCostOrStock(Wheat: 1, Stone: 1, Wool: 1);

            ResultCondition result;

            result = Conditions.CanAfford(player.Resources, cost);
            if (!result.Success)
                return Result<DevelopmentCard>.Fail(result.Reason);

            if (DevelopmentCardsDeckAvailable.Count == 0)
            {
                return Result<DevelopmentCard>.Fail(ConditionFailureReason.NoDevelopmentCardsLeft);
            }

            foreach (var type in cost.ResourceDictionary.Keys)
            {
                player.Resources.ResourceDictionary[type] -= cost.ResourceDictionary[type];
                Bank.ResourceDictionary[type] += cost.ResourceDictionary[type];
            }

            var card = DevelopmentCardsDeckAvailable[0];
            DevelopmentCardsDeckAvailable.RemoveAt(0);
            player.DevelopmentCardsByID.Add(card.ID);

            card.Owner = player;
            card.IsNew = true;

            return Result<DevelopmentCard>.Ok(card);
        }

        public void UseKnight(Player player)
        {
            player.KnightsUsed++;

            CheckChampionship(player, ref MostKnightsUsed, player.KnightsUsed, RequiredKnights, ref KnightChampion);
        }

        public void UseVictoryPoint(Player player)
        {
            player.VictoryPointsCardsUsed++;
            player.CountPoints();
        }

        public List<ResultCardsStolen> UseMonopoly(EnumResourceTypes type)
        {
            var resultMonopolyCard = new List<ResultCardsStolen>();
            var player = CurrentPlayer;

            foreach (Player victim in PlayerList)
            {
                if (victim == player) continue;

                int amount = victim.Resources.ResourceDictionary[type];
                player.Resources.AddExactAmount(type, amount);
                victim.Resources.SubtractExactAmount(type, amount);

                var cardsStolen = new ResultCardsStolen(player.ID, victim.ID, type, amount);
                resultMonopolyCard.Add(cardsStolen);
            }

            return resultMonopolyCard;
        }

        public IReadOnlyList<ResultResourceDistribution> UseYearOfPlenty(ResourceCostOrStock cardsDesired)
        {
            var player = CurrentPlayer;
            var results = new List<ResultResourceDistribution>();

            foreach (var (type, requested) in cardsDesired.ResourceDictionary)
            {
                if (requested <= 0)
                    continue;

                player.Resources.AddExactAmount(type, requested);
                Bank.SubtractExactAmount(type, requested);

                results.Add(new ResultResourceDistribution(player.ID, type, requested, requested));
            }

            return results;
        }

        public Result<ResultBankTrade> PerformBankTrade(EnumResourceTypes offered, EnumResourceTypes desired)
        {
            var player = CurrentPlayer;
            int ratio = FindTradeRatio(offered);

            if (player.Resources.Get(offered) < ratio)
            {
                return Result<ResultBankTrade>.Fail(ConditionFailureReason.CannotAfford);
            }

            if (Bank.Get(desired) < 1)
            {
                return Result<ResultBankTrade>.Fail(ConditionFailureReason.NoResourceCardsLeft);
            }

            player.Resources.SubtractExactAmount(offered, ratio);
            Bank.AddExactAmount(offered, ratio);

            player.Resources.AddExactAmount(desired, 1);
            Bank.SubtractExactAmount(desired, 1);

            return Result<ResultBankTrade>.Ok(new ResultBankTrade(player.ID, offered, desired, ratio));
        }

        public void WinCheck()
        {
            Player player = CurrentPlayer;
            if (player.Points >= RequiredPoints)
            {
                AnyoneHasTenPoints = true;
                GameOver(player);
            }
        }

        public ResultEndGame GameOver(Player winner)
        {
            Dictionary<int, int> playerScoresToIds = new();

            foreach (Player player in PlayerList)
            {
                playerScoresToIds[player.Points] = player.ID;
            }

            var result = new ResultEndGame(playerScoresToIds, winner.ID);

            return result;
        }

        public List<int> GetCurrentPlayerDevelopmentCardIds()
        {
            return CurrentPlayer.DevelopmentCardsByID;
        }

        public Player GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        public Player GetPlayerById(int id)
        {
            return PlayerList.Find(p => p.ID == id);
        }

        public bool GetAfterRoll()
        {
            return AfterRoll;
        }

        public int GetRolledNumber()
        {
            return LastRoll;
        }

        public Queue<Player> GetCardsDiscardingPlayers()
        {
            var playersToDiscard = new Queue<Player>(PlayerList.Where(p => p.Resources.ResourceDictionary.Values.Sum() > 7));

            return playersToDiscard;
        }

        public Dictionary<EnumResourceTypes, bool> CheckResourcesAvailabilityAfterChange(ResourceCostOrStock cardsAlreadySelected)
        {
            var availability = new Dictionary<EnumResourceTypes, bool>();

            foreach (var (type, amount) in Bank.ResourceDictionary)
            {
                int alreadySelected = cardsAlreadySelected.Get(type);
                availability[type] = amount - alreadySelected > 0;
            }

            return availability;
        }

        public Dictionary<EnumResourceTypes, bool> CheckResourcesAvailability()
        {
            var availability = new Dictionary<EnumResourceTypes, bool>();

            foreach (var (type, amount) in Bank.ResourceDictionary)
            {
                bool available = amount > 0;
                availability[type] = available;
            }

            return availability;
        }

        public void SetAfterRollTo(bool afterRoll)
        {
            AfterRoll = afterRoll;
        }
    }
}