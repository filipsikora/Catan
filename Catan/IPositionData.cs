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

        Player? Owner { get; set; }

        bool AccessibleByPlayer(Player player);
    }
}
