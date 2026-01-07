using Catan.Core.Models;
using System.Linq;

namespace Catan.Core.Rules
{
    public static class RulesCardTheft
    {
        public static bool CanSteal(Player victim)
        {
            return victim.Resources.ResourceDictionary.Values.Sum() > 0;
        }
    }
}
