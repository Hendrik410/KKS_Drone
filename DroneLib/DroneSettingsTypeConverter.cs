using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace DroneLibrary
{
    public class DroneSettingsTypeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            Type type = value.GetType();
            FieldInfo[] fields = type.GetFields();

            PropertyDescriptor[] props = new PropertyDescriptor[fields.Length];

            for (int i = 0; i < props.Length; i++)
                props[i] = new SettingPropertyDescriptor(fields[i]);
            return new PropertyDescriptorCollection(props);
        }

        private class SettingPropertyDescriptor : PropertyDescriptor
        {
            private readonly FieldInfo field;
            public SettingPropertyDescriptor(FieldInfo field) : base(field.Name, null)
            {
                this.field = field;
            }
            public override string Category { get { return field.GetCustomAttribute<CategoryAttribute>()?.Category; } }
            public override string Description { get { return field.GetCustomAttribute<DescriptionAttribute>()?.Description; } }
            public override string Name
            {
                get
                {
                    DisplayNameAttribute name = field.GetCustomAttribute<DisplayNameAttribute>();
                    if (name != null)
                        return name.DisplayName;
                    return field.Name;
                }
            }

            public override bool ShouldSerializeValue(object component) { return false; }
            public override void ResetValue(object component) { }
            public override bool IsReadOnly { get { return false; } }
            public override Type PropertyType { get { return field.FieldType; } }
            public override bool CanResetValue(object component) { return false; }
            public override Type ComponentType { get { return typeof(FieldInfo); } }
            public override void SetValue(object component, object value) { field.SetValue(component, value); }
            public override object GetValue(object component) { return field.GetValue(component); }
        }
    }
}
