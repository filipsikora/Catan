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

        
        public bool CanAfford(ResourceCostOrStock other)
        {
            foreach (var entry in ResourceDictionary)
            {
                if (entry.Value < other.ResourceDictionary[entry.Key])
                {
                    return false;
                }
            }
            return true;
        }

        public void ShowResources()
        {
            foreach (var entry in ResourceDictionary)
            {
                Debug.Log($"{entry.Key}: {entry.Value}.");
            }
        }

        public void SubtractCards(ResourceCostOrStock other)
        {
            foreach (var key in ResourceDictionary.Keys)
            {
                ResourceDictionary[key] -= other.ResourceDictionary[key];
            }    
        }

        public void AddCards (GameState game, EnumResourceTypes type, int amount)
        {

            if (!game.Bank.ResourceDictionary.ContainsKey(type) || amount <= 0)
            {
                Debug.Log("Invalid request.");
                return;
            }

            int available = game.Bank.ResourceDictionary[type];
            int toGive = Math.Min(amount, available);

            ResourceDictionary[type] += toGive;
            game.Bank.ResourceDictionary[type] -= toGive;

            if (available < amount)
            {
                Debug.Log($"Not enough {type} in the bank, {Name} received {toGive} {type}.");
            }

            else
            {
                Debug.Log($"{Name} received {toGive} {type}."); 
            }
        }
    }
}
