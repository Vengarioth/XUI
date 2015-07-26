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

            var path = new Path();
            
            for(int i = 0; i < Points.Length; i++)
            {
                double factor = 2000.0;

                path.AddPathSegment(new LineSegment(
                    new Point(Points[i].X / factor, Points[i].Y / factor),
                    new Point(Points[(i + 1) % Points.Length].X / factor, Points[(i + 1) % Points.Length].Y / factor)
                ));
            }

            shape.AddPath(path);

            return shape;
        }
    }
}
