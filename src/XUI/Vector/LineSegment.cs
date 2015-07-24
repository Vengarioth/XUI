using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Vector
{
    public class LineSegment : Segment
    {
        public LineSegment(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
