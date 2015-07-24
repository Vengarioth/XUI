using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Vector
{
    public class Path
    {
        public BlendMode BlendMode { get; set; }
        public CompositMode CompositMode { get; set; }

        public IEnumerable<Segment> Segments { get { return segments; } }

        public Point Start { get { return segments.First().Start; } }

        private List<Segment> segments;

        public Path()
        {
            segments = new List<Segment>();
        }

        public void AddPathSegment(Segment segment)
        {
            segments.Add(segment);
        }
    }
}
