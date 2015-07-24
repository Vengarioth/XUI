using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.Vector.Paths
{
    public class QuadraticCurveSegment : PathSegment
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Point ControlPoint { get; set; }

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
    }
}
