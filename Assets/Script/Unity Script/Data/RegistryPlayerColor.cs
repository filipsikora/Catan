using UnityEngine;

namespace Catan.Unity.Data
{
    public static class RegistryPlayerColor
    {
        public static readonly Color[] Colors =
        {
        Color.red,
        Color.blue,
        Color.white,
        Color.orange
    };

        public static Color GetColor(int playerId)
        {
            return Colors[playerId - 1];
        }
    }
}