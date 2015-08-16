using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point Lerp(double t, Point a, Point b)
        {
            return new Point(
                (b.X - a.X) * t + a.X,
                (b.Y - a.Y) * t + a.Y
            );
        }

        public override string ToString()
        {
            return String.Format("Point ({0}, {1})", X, Y);
        }
    }
}
