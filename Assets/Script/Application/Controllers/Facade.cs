using Catan.Core;
using Catan.Core.Results;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Controllers
{
    public sealed class Facade
    {
        private GameSession _session;

        public Facade(GameSession session)
        {
            _session = session;
        }

        // getters//

        public EnumGamePhases GetCurrentPhase() => _session.GetCurrentPhase();

        public int GetTradeRatioForCurrentPlayer(EnumResourceTypes resource) => _session.GetTradeRatioForCurrentPlayer(resource);

        public int GetCurrentPlayerResourceAmount(EnumResourceTypes resource) => _session.GetCurrentPlayerResourceAmount(resource);

        public int GetCurrentPlayerId() => _session.GetCurrentPlayerId();

        public List<int> GetAdjacentToHexPlayersIds(int hexId) => _session.GetAdjacentToHexPlayersIds(hexId);

        public List<int> GetPossibleVictimsIds() => _session.GetPossibleVictimsIds();

        // use cases//

        public ResultBankTrade BankTrade(EnumResourceTypes offered, EnumResourceTypes desired) => _session.BankTrade(offered, desired);

        public ResultBlockHex BlockHex(int hexId) => _session.BlockHex(hexId);

        public ResultCondition SelectVictim(int victimId) => _session.SelectVictim(victimId);
    }
}