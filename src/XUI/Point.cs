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

        public Point Normalized { get { return Normalize(this); } }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point Normalize(Point p)
        {
            var d = Math.Sqrt((p.X * p.X) + (p.Y * p.Y));
            return new Point(p.X / d, p.Y / d);
        }

        public static Point Middle(Point a, Point b)
        {
            return new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
        }

        public static Point Lerp(double t, Point a, Point b)
        {
            return new Point(
                (b.X - a.X) * t + a.X,
                (b.Y - a.Y) * t + a.Y
            );
        }

        public static Point operator -(Point p)
        {
            return new Point(-p.X, -p.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator -(Point a, double s)
        {
            return new Point(a.X - s, a.Y - s);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator +(Point a, double s)
        {
            return new Point(a.X + s, a.Y + s);
        }

        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        public static Point operator *(Point a, double s)
        {
            return new Point(a.X * s, a.Y * s);
        }

        public static Point operator /(Point a, Point b)
        {
            return new Point(a.X / b.X, a.Y / b.Y);
        }

        public static Point operator /(Point a, double s)
        {
            double num = 1.0 / s;
            return new Point(a.X * num, a.Y * num);
        }

        public override string ToString()
        {
            return String.Format("Point ({0}, {1})", X, Y);
        }
    }
}
