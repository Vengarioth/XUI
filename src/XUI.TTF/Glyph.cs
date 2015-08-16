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

            int from = 0;
            int to = -1;

            for(int e = 0; e < EndPtsOfContours.Length; e++)
            {
                from = to + 1;
                to = EndPtsOfContours[e];
                ExtractPath(shape, from, to);
            }
            
            return shape;
        }

        private void ExtractPath(Shape shape, int from, int to)
        {
            double factor = 2000.0;
            var winding = 0.0;

            int length = to - from;
            Point pa, pb;
            int a, b;

            var path = new Path();

            int i = 0;
            while (i <= length)
            {
                a = i;

                int j = i + 1;

                while (j <= length && (Flags[from + j] & GlyphFlag.OnCurve) != GlyphFlag.OnCurve)
                    j++;

                b = (j) % (length + 1);

                int padding = j - i;

                pa = new Point(Points[from + a].X / factor, Points[from + a].Y / factor);
                pb = new Point(Points[from + b].X / factor, Points[from + b].Y / factor);

                if(padding > 2)
                {
                    //TODO
                    var pc = new Point(Points[from + a + 1].X / factor, Points[from + a + 1].Y / factor);
                    path.AddPathSegment(new QuadraticCurveSegment(pa, pc, pb) { Convex = true });
                }
                else if(padding == 2)
                {
                    var pc = new Point(Points[from + a + 1].X / factor, Points[from + a + 1].Y / factor);
                    path.AddPathSegment(new QuadraticCurveSegment(pa, pc, pb) { Convex = true });
                }
                else
                {
                    path.AddPathSegment(new LineSegment(pa, pb));
                }
                
                winding += (pb.X - pa.X) * (pb.Y + pa.Y);

                i = j;
            }

            if (winding >= 0.0)
                path.CompositMode = CompositMode.Add;
            else
                path.CompositMode = CompositMode.Subtract;

            foreach(var segment in path.Segments)
            {
                if(segment is QuadraticCurveSegment)
                {
                    var qcs = segment as QuadraticCurveSegment;
                    var sign = Math.Sign(((qcs.End.X - qcs.Start.X) * (qcs.ControlPoint.Y - qcs.Start.Y)) - ((qcs.End.Y - qcs.Start.Y) * (qcs.ControlPoint.X - qcs.Start.X)));

                    if(path.CompositMode == CompositMode.Subtract)
                        qcs.Convex = sign >= 0.0 ? false : true;
                    else
                        qcs.Convex = sign >= 0.0 ? true : false;

                }
            }

            shape.AddPath(path);
        }
    }
}
