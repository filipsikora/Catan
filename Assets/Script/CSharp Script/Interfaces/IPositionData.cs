using Catan.Core.Models;

namespace Catan.Core.Interfaces
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