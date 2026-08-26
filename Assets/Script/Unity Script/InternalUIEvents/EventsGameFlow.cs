using Catan.Shared.Data;
using Catan.Unity.Interfaces;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class BankInformationChangedUIEvent : IInternalUIEvents
    {
        public Dictionary<EnumResourceType, int> Bank { get; }
        public BankInformationChangedUIEvent(Dictionary<EnumResourceType, int> bank)
        {
            Bank = bank;
        }
    }
}