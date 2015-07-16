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

        public UIElement Child { get; set; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Update()
        {
            
        }

        public void PerformDrawingOperations()
        {

        }

    }
}
