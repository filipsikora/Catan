using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;
using JetBrains.Annotations;
using NUnit.Framework.Internal;

namespace Catan.Communication.Signals
{
    public class PlayerCountSelectedSignal
    {
        public int PlayerCount;
        public PlayerCountSelectedSignal(int playerCount)
        {
            PlayerCount = playerCount;
        }
    }
}