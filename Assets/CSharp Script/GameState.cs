#nullable enable
using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Catan
{
    public class GameState
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public ResourceCostOrStock Bank { get; set; } = new ResourceCostOrStock(19, 19, 19, 19, 19);

        public List<EnumDevelopmentCardTypes> DevelopmentCardsDeck { get; private set; } = new();

        public bool AnyoneHasTenPoints { get; set; } = false;

        public System.Random Random { get; } = new System.Random();

        public int LastRoll { get; set; } = 0;

        public int Turn { get; set; } = 1;

        public HexMap? Map { get; set; } = null;

        public List<int> FieldNumbersList { get; set; } = new List<int> { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11, 12 };

        public Player? currentPlayer = null;

        public int currentPlayerIndex = 0;

        public Vertex? lastPlacedVillagePosition = null;


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
                Player player = new Player(name);
                player.PlayerColor = colors[i % colors.Count];
                PlayerList.Add(player);

                foreach (var key in player.Resources.ResourceDictionary.Keys.ToList())

                {
                    player.Resources.ResourceDictionary[key] = 4;
                }

            }

            PlayerList = PlayerList.OrderBy(_ => Random.Next()).ToList();
            currentPlayer = PlayerList[0];
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
            WinCheck(currentPlayer);
            Turn++;

            currentPlayerIndex = (currentPlayerIndex + 1) % PlayerList.Count;
            currentPlayer = PlayerList[currentPlayerIndex];


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

            lastPlacedVillagePosition = vertex;

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

            player.CountPoints();

            return true;
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

        public int FindTradeRatio(VisualResourceCard card)
        {
            EnumResourceTypes type = card.Type;

            if (currentPlayer.Ports.Count != 0)
            {
                Port rightPort = Map.PortList.Find(port => port.Type == card.Type);
                Port threeToOnePort = Map.PortList.Find(port => port.Type == null);

                if (currentPlayer.Ports.Contains(rightPort))
                    return 2;

                if (currentPlayer.Ports.Contains(threeToOnePort))
                    return 3;
            }

            return 4;
        }

        public void PrepareDevelopmentDeck()
        {
            DevelopmentCardsDeck.Clear();
            AddCardsToDeck(EnumDevelopmentCardTypes.Knight, 14);
            AddCardsToDeck(EnumDevelopmentCardTypes.VictoryPoint, 5);
            AddCardsToDeck(EnumDevelopmentCardTypes.RoadBuilding, 2);
            AddCardsToDeck(EnumDevelopmentCardTypes.Monopoly, 2);
            AddCardsToDeck(EnumDevelopmentCardTypes.YearOfPlenty, 2);

            DevelopmentCardsDeck = DevelopmentCardsDeck.OrderBy(_ => Random.Next()).ToList();
        }

        public EnumDevelopmentCardTypes? BuyDevelopmentCard(Player player)
        {
            var cost = new ResourceCostOrStock(Wheat: 1, Stone: 1, Wool: 1);

            if (!Conditions.CanAfford(player.Resources, cost))
                return null;

            if (DevelopmentCardsDeck.Count == 0)
            {
                UnityEngine.Debug.Log("No cards left");
                return null;
            }

            foreach (var type in cost.ResourceDictionary.Keys)
            {
                player.Resources.ResourceDictionary[type] -= cost.ResourceDictionary[type];
                Bank.ResourceDictionary[type] += cost.ResourceDictionary[type];
            }

            var card = DevelopmentCardsDeck[0];
            DevelopmentCardsDeck.RemoveAt(0);
            player.DevelopmentCards.Add(card);
            player.DevelopmentCardsNew.Add(card);

            return card;
        }

        public void AddCardsToDeck(EnumDevelopmentCardTypes type, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                DevelopmentCardsDeck.Add(type);
            }
        }

        public void OnKnightUsed()
        {

        }

        public void WinCheck(Player? player)
        {
            if (player.Points > 9)
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
