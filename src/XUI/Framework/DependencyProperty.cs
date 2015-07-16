using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Framework
{
    public class DependencyProperty
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public Type OwnerType { get; private set; }

        private Dictionary<UIElement, object> properties;

        public static DependencyProperty Register(string propertyName, Type propertyType, Type ownerType)
        {
            return new DependencyProperty(propertyName, propertyType, ownerType);
        }

        private DependencyProperty(string name, Type type, Type ownerType)
        {
            Name = name;
            Type = type;
            OwnerType = ownerType;

            properties = new Dictionary<UIElement, object>();
        }

        public object GetValue(UIElement propertyHolder)
        {
            if (!properties.ContainsKey(propertyHolder))
            {
                properties.Add(propertyHolder, default(Type));
                propertyHolder.AddDependencyProperty(this);
            }

            return properties[propertyHolder];
        }

        public void SetValue(UIElement propertyHolder, object value)
        {
            if (properties.ContainsKey(propertyHolder)) {
                properties[propertyHolder] = value;
                return;
            }

            properties.Add(propertyHolder, value);
            propertyHolder.AddDependencyProperty(this);
        }

        internal void ClearProperty(UIElement propertyHolder)
        {
            if (!properties.ContainsKey(propertyHolder))
                return;

            properties.Remove(propertyHolder);
        }
    }
}
