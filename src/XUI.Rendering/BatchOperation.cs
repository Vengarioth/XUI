using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    public class BatchOperation
    {
        public int SourceX { get; private set; }
        public int SourceY { get; private set; }
        public int SourceWidth { get; private set; }
        public int SourceHeight { get; private set; }

        public int TargetX { get; private set; }
        public int TargetY { get; private set; }
        public int TargetWidth { get; private set; }
        public int TargetHeight { get; private set; }
    }
}
