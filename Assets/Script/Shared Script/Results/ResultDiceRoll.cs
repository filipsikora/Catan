using System.Collections.Generic;

namespace Catan.Shared.Results
{
    public sealed class ResultDiceRoll
    {
        public int Roll { get; }
        public IReadOnlyList<ResultResourceDistribution> Distributions { get; }
        public ResultDiceRoll(int roll, IReadOnlyList<ResultResourceDistribution> distributions)
        {
            Roll = roll;
            Distributions = distributions;
        }
    }
}
