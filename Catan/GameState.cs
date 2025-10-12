using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    internal class GameState
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public Dictionary<EnumFieldTypes, int> Bank { get; set; } = new Dictionary<EnumFieldTypes, int>()
        {
            { EnumFieldTypes.Wheat, 19 },
            { EnumFieldTypes.Wood, 19 },
            { EnumFieldTypes.Wool, 19 },
            { EnumFieldTypes.Stone, 19 },
            { EnumFieldTypes.Clay, 19 }
        };

        public bool AnyoneHasTenPoints { get; set; } = false;

        public Random Random { get; } = new Random();

        public int? LastRoll { get; set; } = null;

        public int Turn { get; set; } = 0;

        public HexMap? Map { get; set; } = null;


        public GameState(HexMap map)
        {
            Map = map;
        }


        public void AddResource(Player player, EnumFieldTypes type, int amount)
        {
            if (!Bank.ContainsKey(type) || amount <= 0)
            {
                Console.WriteLine("Invalid request.");
            }

            int available = Bank[type];
            int toGive = Math.Min(available, amount);

            Bank[type] -= toGive;
            player.Resources[type] += toGive;

            if (toGive < amount)
            {
                Console.WriteLine($"There was not enough {type} in the bank, {player.Name} received {toGive} {type}.");
            }
            else
            {
                Console.WriteLine($"{player.Name} received {toGive} {type}.");
            }
        }
    }
}
