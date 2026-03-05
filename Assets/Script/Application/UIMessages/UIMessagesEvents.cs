using Catan.Application.Interfaces;
using Catan.Shared.Data;

namespace Catan.Application.UIMessages
{
    public sealed class PhaseChangedMessage : IUIMessages
    {
        public EnumGamePhases Phase { get; }

        public PhaseChangedMessage(EnumGamePhases phase)
        {
            Phase = phase;
        }
    }
}
