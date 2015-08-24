using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public class ColorBrush : Brush
    {
        public Color Color { get; set; }

        protected override bool OnIsVisible()
        {
            return Color.A > 0.00001;
        }
    }
}
