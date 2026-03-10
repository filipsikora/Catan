using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Catan
{
    public class GameState
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public ResourceCostOrStock Bank { get; set; } = new ResourceCostOrStock(19, 19, 19, 19, 19);

        public bool AnyoneHasTenPoints { get; set; } = false;

        public Random Random { get; } = new Random();

        public int? LastRoll { get; set; } = null;

        public int Turn { get; set; } = 1;

        public HexMap? Map { get; set; } = null;

        public List<int> FieldNumbersList { get; set; } = new List<int> { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11, 12 };

        public int PlayerNumber { get; set; } = 0;


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

            foreach (HexTile hex in Map.HexList)
            {
                if (hex.FieldNumber == LastRoll)
                {
                    foreach (Vertex vertex in hex.AdjacentVertices)
                    {
                       int worth = vertex.Building?.Worth ?? 0;

                        var resourceType = hex.GetResourceType();

                            if (vertex.HasVillage)
                            {
                                worth = 1;
                            }

                            var resourceType = hex.GetResourceType();

                            if (resourceType.HasValue)
                            {
                                vertex.Owner.Resources.AddCards(this, resourceType.Value, worth);
                                Debug.Log($"{vertex.Owner.Name} gets {worth} of {resourceType}");
                            }
                        }
                    }
                }
            }
        }

        public void ReadyPlayer()
        {
            for (int i = 1; i <= PlayerNumber; i++)
            {
                string name = $"Player{i}";
                Player player = new Player(name);
                PlayerList.Add(player);

                foreach (var key in player.Resources.ResourceDictionary.Keys)
                {
                    player.Resources.ResourceDictionary[key] = 7;
                }

                PlayerList = PlayerList.OrderBy(_ => Random.Next()).ToList();
            }
        }

        /*

        public void ReadyBoard()
        {
            ReadyFieldList();
            GiveHexesData(Map.HexList);
        }

        public bool CheckConstructionCondition<T>(Player player, IPositionData position)
            where T : Building, IBuildingData
        {

            var cost = BuildingDataRegistry.Cost[typeof(T)];
            var name = BuildingDataRegistry.Name[typeof(T)];
            var max = BuildingDataRegistry.MaxPerPlayer[typeof(T)];

            if (!player.Resources.CanAfford(cost))
            {
                Debug.Log($"{player.Name} can't afford to build a {name}.");
                return false;
            }

            if (!player.HasAvailable<T>())
            {
                Debug.Log($"{player.Name} has no more {name}s left to build.");
                return false;
            }

            if (!position.AccessibleByPlayer(player))
            {
                Debug.Log($"{player.Name} has no access to this location.");
                return false;
            }

            return true;
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

        public bool BuildVillage(Player player, int id)
        {
            Vertex vertex = Map.GetVertexById(id);
            int turn = Turn;

            if (vertex == null)
            {
                Debug.Log("No edge with this ID exists.");
                return false;
            }

            if (!CheckConstructionCondition<BuildingVillage>(player, vertex))
            {
                return false;
            }

            if (vertex.IsOwned)
            {
                Debug.Log("This spot is already occupied.");
                return false;
            }

            if (!vertex.NoSettlementsInRange())
            {
                Debug.Log("There is a settlement too close to this spot.");
                return false;
            }
             
            PayCost<BuildingVillage>(player);

            var village = new BuildingVillage(player, vertex.X, vertex.Y, vertex);
            player.Buildings.Add(village);

            vertex.Building = village;
            vertex.Owner = player;

            Debug.Log($"{player.Name} has built a village on the vertex {vertex.Id}");
            return true;
        }

        public bool BuildRoad(Player player, int id)
        {
            Edge? edge = Map.GetEdgeById(id);

            if (edge == null)
            {
                Debug.Log("No edge with this ID exists.");
                return false;
            }

            if (!CheckConstructionCondition<BuildingRoad>(player, edge))
            {
                return false;
            }

            if (edge.IsOwned)
            {
                Debug.Log("This spot is already occupied.");
                return false;
            }

            PayCost<BuildingRoad>(player);

            var road = new BuildingRoad(player, edge.X, edge.Y, edge);
            player.Buildings.Add(road);

            edge.Owner = player;

            Debug.Log($"{player.Name} has built a road on the edge {edge.Id}");
            return true;
        }

        public bool UpgradeToCity(Player player, int id)
        {
            Vertex vertex = Map.GetVertexById(id);
            int turn = Turn;

            if (vertex == null)
            {
                Debug.Log("No edge with this ID exists.");
                return false;
            }

            if (!CheckConstructionCondition<BuildingTown>(player, vertex))
            {
                return false;
            }

            if (!(vertex.Owner == player && vertex.Building is BuildingVillage))
            {
                Debug.Log($"You need to choose a village owned by you.");
                return false;
            }

            PayCost<BuildingTown>(player);

            var village = vertex.Building;
            player.Buildings.Remove((Building)village);

            var town = new BuildingTown(player, vertex.X, vertex.Y, vertex);
            player.Buildings.Add(town);

            vertex.Building = town;

            vertex.HasVillage = false;
            vertex.HasTown = true;

            Debug.Log($"{player.Name} has upgraded his village on the vertex {vertex.Id}");
            return true;
        }

        /*
        public void LetPlayerChoose(Player player)
        {
            bool activeTurn = true;

            Debug.Log($"{player.Name}'s turn: " +
              $"1 - build a road" +
              $"2 - build a village" +
              $"3 - upgrade a village" +
              $"4 - end turn");

            while (activeTurn)
            { 

                string decision = Console.ReadLine();

                if (decision != "4")
                {
                    Debug.Log($"Give the position's ID:");
                }

                string stringId = Console.ReadLine();
                int id = int.Parse(stringId);

                switch (decision)
                {
                    case "1":

                        BuildRoad(player, id);
                        break;

                    case "2":

                        BuildVillage(player, id);
                        break;

                    case "3":

                        UpgradeToCity(player, id);
                        break;

                    case "4":
                        activeTurn = false;
                        player.Points = player.CountPoints();
                        break;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;

                }
            }
        }

        public void FirstVillagesAndRoads(Player player)
        {
            Vertex vertex = null;
            bool villagePlaced = false;
            while (!villagePlaced)
            {
                Console.WriteLine($"{player.Name} chooses where to place his free village:");

                string stringvertexid = Console.ReadLine();
                int vertexid = int.Parse(stringvertexid);

                vertex = Map.GetVertexById(vertexid);

                if (vertex == null)
                {
                    Console.WriteLine("No vertex with this ID exists.");
                    continue;
                }

                if (vertex.IsOwned)
                {
                    Console.WriteLine("This spot is already occupied.");
                    continue;
                }

                if (!vertex.NoSettlementsInRange())
                {
                    Console.WriteLine("There is a settlement too close to this spot.");
                    continue;
                }

                var village = new BuildingVillage(player, vertex.X, vertex.Y, vertex);
                player.Buildings.Add(village);

                vertex.Building = village;
                vertex.Owner = player;
                villagePlaced = true;

                Console.WriteLine($"{player.Name} has built a village on the vertex {vertex.Id}");
                break;
            }

            bool roadPLaced = false;
            while (!roadPLaced)
            {
                Console.WriteLine($"{player.Name} chooses where to place his free road:");

                string stringedgeid = Console.ReadLine();
                int edgeid = int.Parse(stringedgeid);

                Edge? edge = Map.GetEdgeById(edgeid);

                if (edge == null)
                {
                    Console.WriteLine("No edge with this ID exists.");
                    continue;
                }

                if (edge.IsOwned)
                {
                    Console.WriteLine("This spot is already occupied.");
                    continue;
                }

                if (!edge.IsNextToVertex(vertex))
                {
                    Console.WriteLine("The road needs to be placed next to the latest village.");
                    continue;
                }

                var road = new BuildingRoad(player, edge.X, edge.Y, edge);
                player.Buildings.Add(road);

                edge.Owner = player;
                roadPLaced = true;

                Console.WriteLine($"{player.Name} has built a village on the edge {edge.Id}");
                break;
            }
        }

        */

        public void WinCheck(Player player)
        {
            if (player.Points > 9)
            {
                AnyoneHasTenPoints = true;
                GameOver(player);
            }
        }

        public void GameOver(Player player)
        {
            Debug.Log($"{player.Name} won with {player.Points}.");
        }
        */
    }
        
}
