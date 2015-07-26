using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public struct BoundsS
    {
        public short MinX { get; set; }
        public short MaxX { get; set; }
        public short MinY { get; set; }
        public short MaxY { get; set; }

        public PointS Min { get { return new PointS(MinX, MinY); } }
        public PointS Max { get { return new PointS(MaxX, MaxY); } }

        public BoundsS(short minX, short maxX, short minY, short maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
    }
}
