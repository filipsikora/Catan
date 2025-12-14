using Catan.Catan;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public class DevelopmentCard
    {
        public bool IsNew = true;

        public Player? Owner = null;

        public EnumDevelopmentCardTypes Type;

        public bool IsUsed = false;

        public int ID;


        public DevelopmentCard(EnumDevelopmentCardTypes type, int id)
        {
            Type = type;
            ID = id;
        }
    }
}
