using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public struct PointF
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PointF(float x, float y)
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
