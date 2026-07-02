using System;

namespace Catan.Unity.Caches
{
    public class ConnectionCache
    {
        Guid? PlayerToken { get; set; }
        Guid? GameId { get; set; }

        public ConnectionCache(Guid? playerToken, Guid? gameId)
        {
            PlayerToken = playerToken;
            GameId = gameId;
        }
    }
}