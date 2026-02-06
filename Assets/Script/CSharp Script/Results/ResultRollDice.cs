using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Results
{
    public sealed class ResultRollDice : ResultBase
    {
        public int Roll { get; }
        public IReadOnlyList<ResultDistributeResources> Distributions { get; }
        public bool RolledSevenButNoVictims { get; }

        private ResultRollDice(int roll, IReadOnlyList<ResultDistributeResources> distributions, EnumGamePhases nextPhase, bool rolledSevenButNoVictims) : base(true, nextPhase)
        {
            Roll = roll;
            Distributions = distributions;
            RolledSevenButNoVictims = rolledSevenButNoVictims;
        }

        public static ResultRollDice Ok(int roll, IReadOnlyList<ResultDistributeResources> distributions, EnumGamePhases nextPhase, bool rolledSevenButNoVictims)
        {
            return new ResultRollDice(roll, distributions, nextPhase, rolledSevenButNoVictims);
        }
    }
}