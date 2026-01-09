using System.Collections.Generic;

namespace Catan.Core.Results
{
    public sealed class ResultRollDice
    {
        public int Roll { get; }
        public IReadOnlyList<ResultDistributeResources> Distributions { get; }
        public ResultRollDice(int roll, IReadOnlyList<ResultDistributeResources> distributions)
        {
            Roll = roll;
            Distributions = distributions;
        }
    }
}