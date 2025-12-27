using Catan.Shared.Data;

namespace Catan.Shared.Results
{
    public class ResultBankTrade
    {
        public int PlayerId;
        public EnumResourceTypes Offered;
        public EnumResourceTypes Desired;
        public int Ratio;
        public ResultBankTrade(int playerId, EnumResourceTypes offered, EnumResourceTypes desired, int ratio)
        {
            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
        }
    }
}