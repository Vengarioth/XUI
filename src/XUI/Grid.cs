using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Framework;

namespace XUI
{
    public class Grid : LayoutElement
    {
        private static DependencyProperty marginProperty = DependencyProperty.Register("Margin", typeof(Grid), typeof(Thickness));

        private List<UIElement> children;

        public Grid()
        {
            children = new List<UIElement>();
        }

        public static Thickness GetMargin(UIElement element)
        {
            return (Thickness)marginProperty.GetValue(element);
        }

        public static void SetMargin(UIElement element, Thickness margin)
        {
            marginProperty.SetValue(element, margin);
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
