using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public abstract class LayoutElement : VisualElement
    {
        protected List<UIElement> children = new List<UIElement>();

        public override Rectangle Arrange(Rectangle availableSpace)
        {
            var size = Measure(new Size(availableSpace.Width, availableSpace.Height));

            return new Rectangle(availableSpace.X, availableSpace.Y, size.Width, size.Height);
        }

        public override Size Measure(Size availableSize)
        {
            List<Size> rows = new List<Size>();

            double availableWidth = availableSize.Width;
            double rowHeight = 0;

            foreach(var child in children)
            {
                var size = child.Measure(availableSize);

                if (size.Width > availableWidth)
                {
                    //break and start a new row
                    rows.Add(new Size(availableSize.Width - availableWidth, rowHeight));
                    availableWidth = availableSize.Width - size.Width;
                    rowHeight = size.Height;
                }
                else
                {
                    //add to current row
                    availableWidth -= size.Width;
                    rowHeight = Math.Max(rowHeight, size.Height);
                }
            }

            //add last row
            rows.Add(new Size(availableSize.Width - availableWidth, rowHeight));

            return new Size(rows.Sum(e => e.Width), rows.Sum(e => e.Height));
        }

        public void Add(UIElement element)
        {
            if (children.Contains(element))
                throw new ArgumentException("Element is already a child of this Grid", "element");

            children.Add(element);
        }

        public void Remove(UIElement element)
        {
            if (!children.Contains(element))
                throw new ArgumentException("Element is not a child of this Grid", "element");

            children.Remove(element);
        }
    }
}
