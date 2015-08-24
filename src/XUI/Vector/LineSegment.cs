using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Vector
{
    public class LineSegment : Segment
    {
        public Point RightNormal
        {
            get
            {
                var dx = End.X - Start.X;
                var dy = End.Y - Start.Y;
                return new Point(dy, -dx).Normalized;
            }
        }

        public Point LeftNormal
        {
            get
            {
                var dx = End.X - Start.X;
                var dy = End.Y - Start.Y;
                return new Point(-dy, dx).Normalized;
            }
        }

        public LineSegment(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
