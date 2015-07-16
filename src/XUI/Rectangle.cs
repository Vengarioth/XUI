using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
