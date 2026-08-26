using Catan.Shared.Data;
using Catan.Unity.Interfaces;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class MyResourcesChangedUIEvent : IInternalUIEvents
    {
        public Dictionary<EnumResourceType, int> Resources { get; }
        public MyResourcesChangedUIEvent(Dictionary<EnumResourceType, int> resources)
        {
            Resources = resources;
        }
    }
}