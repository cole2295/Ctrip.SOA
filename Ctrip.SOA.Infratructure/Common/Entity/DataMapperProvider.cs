using System;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataMapperProvider : DataMapperProviderBase
    {
        // Methods
        public override MapResult<TEntity> Map<TEntity>(IRowDataSource dataSource)
        {
            Type entityType = typeof(TEntity);
            TEntity entity = (TEntity)Activator.CreateInstance(entityType);
            PropertyMapCollection pmc = MapBuilder.GetPropertyMaps(entityType);
            MapResult<TEntity> mapResult = new MapResult<TEntity>(entity);
            foreach (string columanName in dataSource)
            {
                MapResultItem resultItem = mapResult.Items[columanName];
                resultItem.Status = MapResultItemStatus.Fail;
                object target = entity;
                object subTarget = null;
                PropertyMap map = pmc[columanName];
                if (map == null)
                {
                    resultItem.Status = MapResultItemStatus.PropertyNotMatch;
                }
                else
                {
                    try
                    {
                        if (map.KeyParts.Count >= 2)
                        {
                            for (int i = 0; i < (map.KeyParts.Count - 1); i++)
                            {
                                PropertyMap subMap = pmc[map.KeyParts[i]];
                                if (subMap == null)
                                    continue;

                                subTarget = subMap.Property.GetValue(target);
                                if (subTarget == null)
                                {
                                    subTarget = Activator.CreateInstance(subMap.Property.PropertyType);
                                    subMap.Property.SetValue(target, subTarget);
                                }
                                target = subTarget;
                            }
                        }

                        var dataValue = dataSource[columanName];
                        resultItem.OriginalValue = dataValue == DBNull.Value ? null : dataValue;
                        object mappedValue = map.Property.SetValue(target, resultItem.OriginalValue);
                        resultItem.MappedValue = mappedValue;
                        resultItem.Status = MapResultItemStatus.Success;
                    }
                    catch (Exception ex)
                    {
                        resultItem.Status = MapResultItemStatus.Fail;
                        resultItem.Error = ex;
                        throw new DataMappingException(
                            "Column:{0} ColumnType:{1} Fail Mapping To EntityProperty:{2} PropertyType:{3}".FormatWith(
                                columanName,
                                resultItem.OriginalValue.GetType(),
                                map.Key,
                                map.Property.PropertyType),
                            ex);
                    }
                }
            }

            return mapResult;
        }
    }
}
