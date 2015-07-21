using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Framework;

namespace XUI
{
    public abstract class UIElement
    {
        private List<DependencyProperty> dependencyProperties = new List<DependencyProperty>();

        public abstract Size Measure(Size availableSize);

        public abstract Rectangle Arrange(Rectangle availableSpace);

        protected object GetValue(DependencyProperty dependencyProperty)
        {
            return dependencyProperty.GetValue(this);
        }

        protected void SetValue(DependencyProperty dependencyProperty, object value)
        {
            dependencyProperty.SetValue(this, value);
        }

        internal void AddDependencyProperty(DependencyProperty dependencyProperty)
        {
            if (dependencyProperties.Contains(dependencyProperty))
                dependencyProperties.Add(dependencyProperty);
        }

        internal void RemoveFromVisualTree()
        {
            foreach (var dependencyProperty in dependencyProperties)
                dependencyProperty.ClearProperty(this);

            dependencyProperties.Clear();
        }
    }
}
