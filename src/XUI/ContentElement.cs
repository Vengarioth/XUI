using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public abstract class ContentElement : VisualElement
    {
        public UIElement Child { get; set; }

        public override IEnumerable<UIElement> GetChildren()
        {
            if (Child == null)
                return new UIElement[0];
            else
                return new UIElement[] { Child };
        }

        public override Rectangle Arrange(Rectangle availableSpace)
        {
            Space = availableSpace;
            return availableSpace;
        }

        public override Size Measure(Size availableSize)
        {
            return new Size(Width + Margin.Left + Margin.Right, Height + Margin.Top + Margin.Bottom);
        }
    }
}
