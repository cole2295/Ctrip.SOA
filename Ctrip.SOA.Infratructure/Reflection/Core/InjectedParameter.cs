using System;
using Ctrip.SOA.Infratructure.Collections;

namespace Ctrip.SOA.Infratructure
{
    public class InjectedParameter : IKeyedObject
    {
        public InjectedParameter(string name, object value)
        {
            this.Name = name;
            this.Type = value == null ? typeof(object) : value.GetType();
            this.Value = value;
        }

        public InjectedParameter(string name, Type type, object value)
        {
            this.Name = name;
            if (value == null)
            {
                this.Type = type == null ? typeof(object) : type;
            }
            else
            {
                this.Type = type == null ? value.GetType() : type;
            }
            this.Value = value;
        }

        /// <summary>
        /// 唯一性键值
        /// </summary>
        public string Key { get { return this.Name; }}

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; private set; }
    }

    public class InjectedParameterCollection : KeyedObjectCollection<InjectedParameter>
    {
    }
}