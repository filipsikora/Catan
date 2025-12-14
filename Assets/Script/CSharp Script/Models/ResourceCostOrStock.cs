#nullable enable
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
    public class ResourceCostOrStock
    {

        public ResourceCostOrStock(int Wheat = 0, int Wood = 0, int Wool = 0, int Stone = 0, int Clay = 0)
        {
            ResourceDictionary = new Dictionary<EnumResourceTypes, int>()
            {
                { EnumResourceTypes.Wheat, Wheat },
                { EnumResourceTypes.Wood, Wood },
                { EnumResourceTypes.Wool, Wool },
                { EnumResourceTypes.Stone, Stone },
                { EnumResourceTypes.Clay, Clay }
            };
        }

        public Dictionary<EnumResourceTypes, int> ResourceDictionary { get; set; }

        public string? Name { get; set; } = null;

        public void ShowResources()
        {
            foreach (var entry in ResourceDictionary)
            {
                UnityEngine.Debug.Log($"{entry.Key}: {entry.Value}.");
            }
        }

        public void SubtractCards(ResourceCostOrStock other)
        {
            foreach (var key in ResourceDictionary.Keys.ToList())
            {
                ResourceDictionary[key] -= other.ResourceDictionary[key];
            }    
        }

        public void AddCards(ResourceCostOrStock other)
        {
            foreach (var key in ResourceDictionary.Keys.ToList())    
            {
                ResourceDictionary[key] += other.ResourceDictionary[key];
            }
        }

        public void AddSingleType(EnumResourceTypes type, int amount)
        {
            ResourceDictionary[type] += amount;
        }

        public void SubtractSingleType(EnumResourceTypes type, int amount)
        {
            ResourceDictionary[type] -= amount;
        }

        public void AddCardsFromTheBank (GameState game, EnumResourceTypes type, int amount)
        {

            if (!game.Bank.ResourceDictionary.ContainsKey(type) || amount <= 0)
            {
                UnityEngine.Debug.Log("Invalid request.");
                return;
            }

            int available = game.Bank.ResourceDictionary[type];
            int toGive = Math.Min(amount, available);

            ResourceDictionary[type] += toGive;
            game.Bank.ResourceDictionary[type] -= toGive;

            if (available < amount)
            {
                UnityEngine.Debug.Log($"Not enough {type} in the bank, {Name} received {toGive} {type}.");
            }

            else
            {
                UnityEngine.Debug.Log($"{Name} received {toGive} {type}."); 
            }
        }

        public bool HasEnoughCards(ResourceCostOrStock other)
        {
            foreach (var entry in other.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int required = entry.Value;

                if (required <= 0)
                    continue;

                int available = ResourceDictionary.ContainsKey(type) ? ResourceDictionary[type] : 0;

                if (available < required)
                    return false;
            }

            return true;
        }
    }
}
