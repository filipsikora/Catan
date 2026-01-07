#nullable enable
using Catan.Shared.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Catan.Core.Models
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

        public Dictionary<EnumResourceTypes, int> ResourceDictionary { get; }

        public string? Name { get; set; } = null;

        public int Get(EnumResourceTypes type)
        {
            return ResourceDictionary.TryGetValue(type, out var v) ? v : 0;
        }

        public int SubtractUpTo(EnumResourceTypes type, int amountWanted)
        {
            if (amountWanted < 0)
                return (0);

            int available = Get(type);
            int actualAmount = Math.Min(amountWanted, available);

            ResourceDictionary[type] -= actualAmount;

            return actualAmount;
        }

        public void AddExactAmount(EnumResourceTypes type, int amount)
        {
            if (amount < 0)
                return;

            ResourceDictionary[type] += amount;
        }

        public void SubtractExactAmount(EnumResourceTypes type, int amount)
        {
            if (amount < 0)
                return;

            ResourceDictionary[type] -= amount;
        }

        public void AddExact(ResourceCostOrStock other)
        {
            foreach (var (type, amount) in other.ResourceDictionary)
            {
                if (amount < 0)
                {
                    Debug.Assert(amount >= 0);
                    continue;
                }

                ResourceDictionary[type] += amount;
            }
        }

        public void SubtractExact(ResourceCostOrStock other)
        {
            foreach (var (type, amount) in other.ResourceDictionary)
            {
                if (amount < 0)
                {
                    Debug.Assert(amount >= 0);
                    continue;
                }

                ResourceDictionary[type] -= amount;
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

        public ResourceCostOrStock Clone()
        {
            var copy = new ResourceCostOrStock();

            foreach (var kv in ResourceDictionary)
            {
                copy.ResourceDictionary[kv.Key] = kv.Value;
            }

            return copy;
        }

        public void Clear()
        {
            foreach (var (key, value) in ResourceDictionary)
            {
                ResourceDictionary[key] = 0;
            }
        }

        public int Total()
        {
            return ResourceDictionary.Values.Sum();
        }

        public Dictionary<EnumResourceTypes, int> ToDictionary()
        {
            var toDictionary = new Dictionary<EnumResourceTypes, int>();

            foreach (var (key, value) in ResourceDictionary)
            {
                toDictionary.Add(key, value);
            }

            return toDictionary;
        }
    }
}
