using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Vector;

namespace XUI.TTF
{
    public class Glyph
    {
        public static Glyph Empty { get { return new Glyph(new PointS[0], new GlyphFlag[0], new byte[0], new ushort[0], new BoundsS()); } }

        public PointS[] Points { get; private set; }
        public GlyphFlag[] Flags { get; private set; }
        public byte[] Instructions { get; private set; }
        public ushort[] EndPtsOfContours { get; private set; }
        public BoundsS Bounds { get; private set; }
        
        public Glyph(PointS[] points, GlyphFlag[] flags, byte[] instructions, ushort[] endPtsOfContours, BoundsS bounds)
        {
            Points = points;
            Flags = flags;
            Instructions = instructions;
            EndPtsOfContours = endPtsOfContours;
            Bounds = bounds;
        }

        public Shape GetAsShape()
        {
            var shape = new Shape();
            double factor = 2000.0;

            int from = 0;
            int to = -1;

            for(int e = 0; e < EndPtsOfContours.Length; e++)
            {
                from = to + 1;
                to = EndPtsOfContours[e];

                Point a, b;

                var path = new Path();
                var winding = 0.0;
                for (int i = from; i < to; i++)
                {
                    a = new Point(Points[i].X / factor, Points[i].Y / factor);
                    b = new Point(Points[i + 1].X / factor, Points[i + 1].Y / factor);
                    path.AddPathSegment(new LineSegment(a, b));

                    winding += (b.X - a.X) * (b.Y + a.Y);
                }

                a = new Point(Points[to].X / factor, Points[to].Y / factor);
                b = new Point(Points[from].X / factor, Points[from].Y / factor);
                path.AddPathSegment(new LineSegment(a, b));
                winding += (b.X - a.X) * (b.Y + a.Y);

                if (winding >= 0.0)
                    path.CompositMode = CompositMode.Add;
                else
                    path.CompositMode = CompositMode.Subtract;

                shape.AddPath(path);
            }
            
            return shape;
        }
    }
}
