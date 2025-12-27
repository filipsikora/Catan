using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Communication.Commands
{
    public class BankTradeCanceledCommand : ICommand { }

    public class BankTradeOfferedResourceSelected : ICommand
    {
        public EnumResourceTypes Type;
        public BankTradeOfferedResourceSelected(EnumResourceTypes type)
        {
            Type = type;
        }
    }

    public class BankTradeDesiredResourceSelected : ICommand
    {
        public EnumResourceTypes Type;
        public BankTradeDesiredResourceSelected(EnumResourceTypes type)
        {
            Type = type;
        }
    }

    public class BankTradeCommand : ICommand { }
}