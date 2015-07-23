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

        public override IEnumerable<UIElement> GetChildren()
        {
            return children.AsEnumerable();
        }

        public override Rectangle Arrange(Rectangle availableSpace)
        {
            double totalHeight = 0;
            double availableWidth = availableSpace.Width;
            double currentRowHeight = 0;

            foreach (var child in children)
            {
                var size = child.Measure(new Size(availableSpace.Width, availableSpace.Height));

                if (size.Width > availableWidth)
                {
                    //break and start a new row
                    totalHeight += currentRowHeight;
                    
                    child.Arrange(new Rectangle(0,
                    totalHeight,
                    size.Width,
                    size.Height));

                    availableWidth = availableSpace.Width - size.Width;
                    currentRowHeight = size.Height;
                }
                else
                {
                    child.Arrange(new Rectangle(availableSpace.Width - availableWidth,
                    totalHeight,
                    size.Width,
                    size.Height));

                    //add to current row
                    availableWidth -= size.Width;
                    currentRowHeight = Math.Max(currentRowHeight, size.Height);
                }
            }

            Space = availableSpace;

            return availableSpace;
        }

        public override Size Measure(Size availableSize)
        {
            double maxWidth = 0;
            double totalHeight = 0;

            double availableWidth = availableSize.Width;
            double rowHeight = 0;

            foreach(var child in children)
            {
                var size = child.Measure(availableSize);

                if (size.Width > availableWidth)
                {
                    //break and start a new row
                    maxWidth = Math.Max(maxWidth, availableSize.Width - availableWidth);
                    totalHeight += rowHeight;
                    
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
            maxWidth = Math.Max(maxWidth, availableSize.Width - availableWidth);
            totalHeight += rowHeight;

            return new Size(maxWidth, totalHeight);
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
