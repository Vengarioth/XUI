using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public class Canvas
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Initialize()
        {
            
        }

        public void PerformDrawingOperations()
        {
            GraphicsContext.Assert();
        }

    }
}
