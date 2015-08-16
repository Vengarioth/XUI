using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Vector
{
    public class QuadraticCurveSegment : Segment
    {
        public Point ControlPoint { get; set; }
        public bool Convex { get; set; }

        public Point P1
        {
            get { return new Point(Start.X + (ControlPoint.X - Start.X) * 2 / 3, Start.Y + (ControlPoint.Y - Start.Y) * 2 / 3); }
        }

        public Point P2
        {
            get { return new Point(ControlPoint.X + (End.X - ControlPoint.X) / 3, ControlPoint.Y + (End.Y - ControlPoint.Y) / 3); }
        }

        public QuadraticCurveSegment(Point start, Point controlPoint, Point end)
        {
            Start = start;
            ControlPoint = controlPoint;
            End = end;
        }

        public Point GetPoint(double t)
        {
            return new Point(
                (1.0 - t) * (1.0 - t) * Start.X + 2.0 * (1.0 - t) * t * ControlPoint.X + t * t * End.X,
                (1.0 - t) * (1.0 - t) * Start.Y + 2.0 * (1.0 - t) * t * ControlPoint.Y + t * t * End.Y
            );
        }

        public QuadraticCurveSegment[] Split(double t)
        {
            var p = new List<Point>();
            var q = new List<Point>();
            var _p = new List<Point>();
            
            p.Add(Start);
            p.Add(ControlPoint);
            p.Add(End);

            q.Add(p[0]);
            q.Add(p[1]);
            q.Add(p[2]);

            while (p.Count > 1)
            {
                _p = new List<Point>();

                for(int i = 0; i < p.Count - 1; i++)
                {
                    var pt = Point.Lerp(t, p[i], p[i + 1]);
                    q.Add(pt);
                    _p.Add(pt);
                }
                p = _p;
            }

            var a = new QuadraticCurveSegment(q[0], q[3], q[5]) { Convex = Convex };
            var b = new QuadraticCurveSegment(q[5], q[4], q[2]) { Convex = Convex };

            return new QuadraticCurveSegment[] { a, b };
        }
    }
}
