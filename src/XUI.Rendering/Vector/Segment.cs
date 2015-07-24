using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.Vector
{
    public abstract class Segment
    {
        public BlendMode BlendMode { get; set; }
        public CompositMode CompositMode { get; set; }

        public Segment()
        {

        }

        public abstract void Draw();
    }
}
