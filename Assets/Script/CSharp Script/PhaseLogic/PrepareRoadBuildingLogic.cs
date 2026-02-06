using Catan.Core.Conditions;
using Catan.Core.Results;
using Catan.Shared.Data;
using System;

namespace Catan.Core.PhaseLogic
{
    public sealed class PrepareRoadBuiliding : BaseLogic
    {
        public PrepareRoadBuiliding(GameSession session) : base(session) { }

        public ResultCondition Handle()
        {
            var player = Session.GetCurrentPlayer();
            var roadsAvailable = Session.GetCurrentPlayersRoadsLeft();
            var result = ConditionsPlayer.PlayerExists(player);

            if (!result.Success)
            {
                return ResultCondition.Fail(result.Reason);
            }

            var _roadsToBuild = Math.Min(2, roadsAvailable);

            Session.CreateRoadBuildingContext(_roadsToBuild);

            return ResultCondition.Ok(EnumGamePhases.None);
        }
    }
}
