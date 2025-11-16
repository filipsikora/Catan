#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public interface IPositionData
    {
        float X { get; }

        float Y { get; }

        int Id { get; set; }

        Player? Owner { get; set; }

        bool AccessibleByPlayer(Player player);
    }
}
