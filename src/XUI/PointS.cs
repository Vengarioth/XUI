using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public struct PointS
    {
        public short X { get; set; }
        public short Y { get; set; }

        public PointS(short x, short y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return String.Format("Point ({0}, {1})", X, Y);
        }
    }
}
