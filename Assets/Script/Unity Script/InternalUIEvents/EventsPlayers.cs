using Catan.Unity.Interfaces;
using Catan.Unity.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class PlayerStateChangedUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerStateChangedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class PlayerClickedUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerClickedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class PlayerStateReceivedUIEvent : IInternalUIEvents
    {
        public MyPlayerModel Player { get; }

        public PlayerStateReceivedUIEvent(MyPlayerModel player)
        {
            Player = player;
        }
    }

    public sealed class OtherPlayersReceivedUIEvent : IInternalUIEvents
    {
        public List<OtherPlayerModel> OtherPlayers { get; }

        public OtherPlayersReceivedUIEvent(List<OtherPlayerModel> otherPlayers)
        {
            OtherPlayers = otherPlayers;
        }
    }
}