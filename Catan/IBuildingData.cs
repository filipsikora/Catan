using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public interface IBuildingData
    {
        static abstract ResourceCostOrStock Cost { get; }

        static abstract int MaxPerPlayer { get; }

        static abstract string Name { get; }
<<<<<<< Updated upstream
=======

        static abstract int Worth { get; } 
>>>>>>> Stashed changes
    } 
}
