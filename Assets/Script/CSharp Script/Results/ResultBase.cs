using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public abstract class ResultBase
    {
        public bool Success { get; }
        public EnumGamePhases? NextPhase { get; }
        
        protected ResultBase(bool success, EnumGamePhases? nextPhase)
        {
            Success = success;
            NextPhase = nextPhase;
        }
    }
}