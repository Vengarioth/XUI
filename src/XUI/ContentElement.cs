using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public abstract class ContentElement : VisualElement
    {
        public override Rectangle Arrange(Rectangle availableSpace)
        {
            return availableSpace;
        }

        public override Size Measure(Size availableSize)
        {
            return new Size(Width + Margin.Left + Margin.Right, Height + Margin.Top + Margin.Bottom);
        }
    }
}
