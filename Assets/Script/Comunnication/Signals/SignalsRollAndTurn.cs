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
    public class RequestDiceRollSignal { }

    public class DiceRolledSignal
    {
        public int RolledNumber { get; }
        public DiceRolledSignal(int rolledNumber)
        {
            RolledNumber = rolledNumber;
        }
    }

    public class RequestEndTurnSignal { }

    public class TurnEndedSignal { }
}
