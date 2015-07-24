using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.Vector
{
    public class Shape
    {
        private List<Segment> segments;

        public Shape()
        {
            segments = new List<Segment>();
        }

        public void AddSegment(Segment segment)
        {
            segments.Add(segment);
        }

        public void RemoveSegment(Segment segment)
        {
            segments.Remove(segment);
        }

        public void Draw()
        {

        }
    }
}
