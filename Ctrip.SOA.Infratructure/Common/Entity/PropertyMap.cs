using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Ctrip.SOA.Infratructure.Reflection.Dynamic;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PropertyMap
    {
        #region [ Constructors ]

        public PropertyMap(string key, PropertyInfo property)
        {
            this.Key = key;
            this.Property = DynamicProperty.Create(property);
            this.Converter = TypeDescriptor.GetConverter(this.Property.PropertyType);
            string[] keysSplited = key.Split(new char[] { MapConsts.SeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            this.KeyParts = new List<string>();
            string tempKey = string.Empty;
            foreach (string keySplited in keysSplited)
            {
                tempKey = string.IsNullOrEmpty(tempKey) ? keySplited : string.Format("{0}{1}{2}", tempKey, MapConsts.SeparatorChar, keySplited);
                this.KeyParts.Add(tempKey);
            }
        }

        #endregion

        #region [ Properties ]

        public TypeConverter Converter { get; set; }

        public string Key { get; private set; }

        public List<string> KeyParts { get; set; }

        public IDynamicProperty Property { get; private set; }

        #endregion

        #region [ Methods ]

        public void SetValue(object obj, object value)
        {
            Type sourceType = (value == null) ? typeof(string) : value.GetType();
            if (this.Converter.CanConvertFrom(sourceType))
            {
                object newValue = this.Converter.ConvertFrom(value);
                this.Property.SetValue(obj, newValue);
            }
        }

        #endregion
    }
}