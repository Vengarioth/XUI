using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Framework;

namespace XUI
{
    public abstract class VisualElement : UIElement
    {
        private static DependencyProperty backgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(VisualElement), defaultValue: new Color());
        public Brush Background
        {
            get { return (Brush)GetValue(backgroundProperty); }
            set { SetValue(backgroundProperty, value); }
        }

        private static DependencyProperty marginProperty = DependencyProperty.Register("Margin", typeof(Thickness), typeof(VisualElement), defaultValue: new Thickness());
        public Thickness Margin
        {
            get { return (Thickness)GetValue(marginProperty); }
            set { SetValue(marginProperty, value); }
        }

        private static DependencyProperty widthProperty = DependencyProperty.Register("Width", typeof(double), typeof(VisualElement), defaultValue: 0.0);
        public double Width
        {
            get { return (double)GetValue(widthProperty); }
            set { SetValue(widthProperty, value); }
        }

        private static DependencyProperty heightProperty = DependencyProperty.Register("Height", typeof(double), typeof(VisualElement), defaultValue: 0.0);
        public double Height
        {
            get { return (double)GetValue(heightProperty); }
            set { SetValue(heightProperty, value); }
        }
    }
}
