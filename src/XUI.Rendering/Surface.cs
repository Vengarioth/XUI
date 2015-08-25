using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    public class Surface
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Surface(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
