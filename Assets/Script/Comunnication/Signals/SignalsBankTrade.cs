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
    public class BankTradeRatioChangedSignal
    {
        public int Ratio { get; }
        public bool Possible { get; }
        public EnumResourceTypes? Resource { get; }

        public BankTradeRatioChangedSignal(int ratio, bool possible, EnumResourceTypes? resource)
        {
            Ratio = ratio;
            Possible = possible;
            Resource = resource;
        }
    }

    public class BankTradeCompletedSignal { }

    public class BankTradeCanceledSignal { }

    public class RequestBankTradeSignal { }
}