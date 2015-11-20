using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ctrip.SOA.Infratructure.Reflection.Dynamic;

namespace Ctrip.SOA.Infratructure.Entity
{
    public class MapResult
    {
        public MapResult(Type entityType)
        {
            this.EntityType = entityType;
            this.Items = new MapResultItemCollection();
        }

        public Type EntityType { get; set; }
        public MapResultItemCollection Items { get; set; }
    }

    public class MapResultItemCollection : KeyedCollection<string, MapResultItem>
    {
        protected override string GetKeyForItem(MapResultItem item)
        {
            return item.ColumnName;
        }

        public new MapResultItem this[string propertyName]
        {
            get
            {
                if (this.Contains(propertyName))
                {
                    return base[propertyName];
                }

                MapResultItem item = new MapResultItem(propertyName, MapResultItemStatus.Fail);
                this.Add(item);
                return item;
            }
        }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MapResultItem
    {
        public MapResultItem()
        {
        }

        public MapResultItem(string columnName, MapResultItemStatus status)
        {
            this.ColumnName = columnName;
            this.Status = status;
        }

        public string ColumnName { get; set; }
        public MapResultItemStatus Status { get; set; }
        public Exception Error { get; set; }

        public object OriginalValue { get; set; }
        public object MappedValue { get; set; }
    }

    public enum MapResultItemStatus
    {
        Success,
        PropertyNotMatch,
        Fail
    }
}