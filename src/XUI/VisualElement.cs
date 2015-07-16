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
        private static DependencyProperty backgroundProperty = DependencyProperty.Register("background", typeof(Brush), typeof(VisualElement));
        public Brush Background
        {
            get { return (Brush)GetValue(backgroundProperty); }
            set { SetValue(backgroundProperty, value); }
        }
    }
}
