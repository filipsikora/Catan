#nullable enable
using Catan.Catan;
using Catan.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Catan
{
    public class GameState
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public ResourceCostOrStock Bank { get; set; } = new ResourceCostOrStock(19, 19, 19, 19, 19);

        public List<DevelopmentCard> DevelopmentCardsDeckAvailable { get; private set; } = new();
        public List<DevelopmentCard> DevelopmentCardsDeckAll { get; private set; } = new();

        public bool AnyoneHasTenPoints { get; set; } = false;

        public Queue<int> FirstRoundsIndices { get; set; } = new();

        public System.Random Random { get; } = new System.Random();

        public int LastRoll { get; set; } = 0;

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

                if (hexList[index].FieldType != EnumFieldTypes.Desert)
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

        public void RollAndServePlayers()
        {
            RollDice();

            UnityEngine.Debug.Log("chuj");

            if (Map == null) return;

            foreach (HexTile hex in Map.HexList)
            {
                if (hex.FieldNumber == LastRoll && !hex.isBlocked)
                {
                    foreach (Vertex vertex in hex.AdjacentVertices)
                    {
                        if (vertex.IsOwned)
                        {
                            int worth = 0;

                            if (vertex.HasTown)
                            {
                                worth = 2;
                            }

                            if (vertex.HasVillage)
                            {
                                worth = 1;
                            }

                            var resourceType = hex.GetResourceType();

                            if (resourceType.HasValue)
                            {
                                vertex.Owner?.Resources.AddCardsFromTheBank(this, resourceType.Value, worth);
                                UnityEngine.Debug.Log($"{vertex.Owner?.Name} gets {worth} of {resourceType} from hex {hex.FieldNumber}");
                            }
                        }
                    }
                }
            }
        }

        public void ReadyPlayer(int playerNumber)
        {
            var colors = new List<Color>
                {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow
                };

            for (int i = 1; i <= playerNumber; i++)
            {
                string name = $"Player{i}";
                Player player = new Player(name, i);
                player.PlayerColor = colors[i % colors.Count];
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
            foreach (var hexTile in Map.HexList)
            {
                hexTile.isBlocked = false;
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
            else
            {
                UnityEngine.Debug.LogWarning("Map is null in ReadyBoard()");
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
            WinCheck(CurrentPlayer);
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

        public bool BuildFreeVillage(Player player, Vertex vertex)
        {
            if (!Conditions.PositionExists(vertex.Id, id => Map.GetVertexById(id))) return false;
            if (!Conditions.IsNotOwned(vertex)) return false;
            if (!Conditions.NoSettlementsInRange(vertex)) return false;

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

            return true;
        }

        public bool BuildFreeRoad(Player player, Edge edge)
        {
            if (!Conditions.PositionExists(edge.Id, id => Map.GetEdgeById(id))) return false;
            if (!Conditions.IsNotOwned(edge)) return false;
            if (!Conditions.HasAccessToPosition(player, edge)) return false;

            var road = new BuildingRoad(player, edge.X, edge.Y, edge);
            player.Buildings.Add(road);

            edge.Owner = player;

            player.CountPoints();

            return true;
        }

        public bool BuildVillage(Player player, Vertex vertex)
        {
            if (!Conditions.PositionExists(vertex.Id, id => Map.GetVertexById(id))) return false;
            if (!Conditions.HasAccessToPosition(player, vertex)) return false;
            if (!Conditions.IsNotOwned(vertex)) return false;
            if (!Conditions.NoSettlementsInRange(vertex)) return false;
            if (!Conditions.HasAvailable<BuildingVillage>(player)) return false;
            if (!Conditions.CanAfford(player.Resources, BuildingDataRegistry.Cost[typeof(BuildingVillage)])) return false;

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

            return true;
        }

        public bool BuildRoad(Player player, Edge edge)
        {
            if (!Conditions.PositionExists(edge.Id, id => Map.GetEdgeById(id))) return false;
            if (!Conditions.HasAccessToPosition(player, edge)) return false;
            if (!Conditions.IsNotOwned(edge)) return false;
            if (!Conditions.HasAvailable<BuildingRoad>(player)) return false;
            if (!Conditions.CanAfford(player.Resources, BuildingDataRegistry.Cost[typeof(BuildingRoad)])) return false;

            PayCost<BuildingRoad>(player);

            var road = new BuildingRoad(player, edge.X, edge.Y, edge);
            player.Buildings.Add(road);

            edge.Owner = player;

            UpdateRoadChampion();

            return true;
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

        public bool UpgradeVillage(Player player, Vertex vertex)
        {
            if (!Conditions.PositionExists(vertex.Id, id => Map.GetVertexById(id))) return false;
            if (!Conditions.HasVillage(player, vertex)) return false;
            if (!Conditions.HasAvailable<BuildingTown>(player)) return false;
            if (!Conditions.CanAfford(player.Resources, BuildingDataRegistry.Cost[typeof(BuildingTown)])) return false;

            PayCost<BuildingTown>(player);

            var town = new BuildingTown(player, vertex.X, vertex.Y, vertex);
            var village = player.Buildings.FirstOrDefault(b => b is BuildingVillage v && v.Vertex == vertex);
            player.Buildings.Remove(village);
            player.Buildings.Add(town);

            vertex.HasVillage = false;
            vertex.HasTown = true;

            player.CountPoints();

            return true;
        }

        public int FindTradeRatio(EnumResourceTypes type)
        {
            if (CurrentPlayer.Ports.Count != 0)
            {
                Port rightPort = Map.PortList.Find(port => port.Type == type);
                Port threeToOnePort = Map.PortList.Find(port => port.Type == null);

                if (CurrentPlayer.Ports.Contains(rightPort))
                    return 2;

                if (CurrentPlayer.Ports.Contains(threeToOnePort))
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

        public DevelopmentCard BuyDevelopmentCard(Player player)
        {
            var cost = new ResourceCostOrStock(Wheat: 1, Stone: 1, Wool: 1);

            if (!Conditions.CanAfford(player.Resources, cost))
                return null;

            if (DevelopmentCardsDeckAvailable.Count == 0)
            {
                UnityEngine.Debug.Log("No cards left");
                return null;
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

            return card;
        }

        public void OnKnightUsed(Player player)
        {
            player.KnightsUsed++;

            CheckChampionship(player, ref MostKnightsUsed, player.KnightsUsed, RequiredKnights, ref KnightChampion);
        }

        public void OnVictoryPointUsed(Player player)
        {
            player.VictoryPointsCardsUsed++;
            player.CountPoints();
        }

        public void OnMonopolyUsed(Player player, EnumResourceTypes type)
        {
            foreach (Player victim in PlayerList)
            {
                if (victim == player) continue;

                int amount = victim.Resources.ResourceDictionary[type];
                player.Resources.AddSingleType(type, amount);
                victim.Resources.SubtractSingleType(type, amount);
                UnityEngine.Debug.Log($"{player} steals {amount} {type} from {victim}");
            }
        }

        public void OnYearOfPlentyUsed(Player player, ResourceCostOrStock cardsDesired)
        {
            player.Resources.AddCards(cardsDesired);
            Bank.SubtractCards(cardsDesired);
            UnityEngine.Debug.Log($"{player} received {cardsDesired}");
        }

        public void WinCheck(Player? player)
        {
            if (player.Points >= RequiredPoints)
            {
                AnyoneHasTenPoints = true;
                GameOver(player);
            }
        }

        public void GameOver(Player player)
        {
            UnityEngine.Debug.Log($"{player.Name} won with {player.Points}.");
        }
    }
}
