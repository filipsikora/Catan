using System;

namespace Catan.Unity.Caches
{
    public class ConnectionCache
    {
        public Guid? PlayerToken { get; set; }
        public Guid? GameId { get; set; }

        public ConnectionCache(Guid? playerToken, Guid? gameId)
        {
            PlayerToken = playerToken;
            GameId = gameId;
        }
    }
}