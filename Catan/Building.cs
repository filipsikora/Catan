using Catan.Catan;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public abstract class Building
    {

        public float X { get; set; }

        public float Y { get; set; }

        public Player? Owner { get; set; }


        public Building(float x, float y, Player owner)
        {
            X = x;
            Y = y;
            Owner = owner;
        }

        
    }
}
