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
        public object DefaultValue { get; private set; }

        private Dictionary<UIElement, object> properties;

        public static DependencyProperty Register(string propertyName, Type propertyType, Type ownerType, object defaultValue = null)
        {
            return new DependencyProperty(propertyName, propertyType, ownerType, defaultValue);
        }

        private DependencyProperty(string name, Type type, Type ownerType, object defaultValue)
        {
            Name = name;
            Type = type;
            OwnerType = ownerType;
            DefaultValue = defaultValue;

            properties = new Dictionary<UIElement, object>();
        }

        public object GetValue(UIElement propertyHolder)
        {
            if (!properties.ContainsKey(propertyHolder))
            {

                properties.Add(propertyHolder, DefaultValue);
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
