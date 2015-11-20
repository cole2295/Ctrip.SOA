using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class MapBuilder
    {
        #region [ Fields ]

        private static object s_SyncObj = new object();

        #endregion

        #region [ Methods ]

        public static PropertyMapCollection GetPropertyMaps(Type objType)
        {
            PropertyMapCollection pmc = PropertyMapCache.GetMaps(objType);
            if (pmc == null)
            {
                lock (s_SyncObj)
                {
                    pmc = PropertyMapCache.GetMaps(objType);
                    if (pmc == null)
                    {
                        pmc = BuildPropertyMaps(objType);
                    }
                }
            }
            return pmc;
        }

        public static PropertyMapCollection BuildPropertyMaps(Type objType)
        {
            PropertyInfo[] propertyInfos = objType.GetProperties();
            //added by Pluto Mei. 2014-1-6
            //to support field in map
            if (propertyInfos.Length > 0)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string propertyName = propertyInfo.Name;
                    Type propertyType = propertyInfo.PropertyType;

                    PropertyMap map = new PropertyMap(propertyName, propertyInfo);
                    PropertyMapCache.AddMap(objType, map);
                    if (!propertyType.HasElementType && IsComplexType(propertyType))
                    {
                        List<string> parentNames = new List<string> {
                        propertyName
                    };
                        BuildSubPropertyMaps(objType, parentNames, propertyType);
                    }
                }
            }
            else
            {
                foreach (FieldInfo fieldInfo in objType.GetFields())
                {
                    //add by Pluto Mei.2014-1-6
                    //TODO: need reflect to support field
                    //old design is strong bind with propertyType
                    //to support field will cost many workload,so delay it.
                }
            }
            return PropertyMapCache.GetMaps(objType);
        }

        public static void BuildSubPropertyMaps(Type rootType, List<string> parentNames, Type propertyType)
        {
            PropertyInfo[] propertyInfos = propertyType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string subPropertyName = propertyInfo.Name;
                Type subPropertyType = propertyInfo.PropertyType;

                PropertyMap map = new PropertyMap(BuildMapKey(parentNames, subPropertyName), propertyInfo);
                PropertyMapCache.AddMap(rootType, map);
                if (!propertyType.HasElementType && IsComplexType(subPropertyType))
                {
                    parentNames.Add(subPropertyName);
                    BuildSubPropertyMaps(rootType, parentNames, subPropertyType);
                    parentNames.Remove(subPropertyName);
                }
            }
        }

        private static string BuildMapKey(List<string> parentNames, string propertyName)
        {
            List<string> pathNameList = new List<string>(parentNames) {
                propertyName
            };
            string key = string.Empty;
            foreach (string name in pathNameList)
            {
                key = string.IsNullOrEmpty(key) ? name : string.Format("{0}{1}{2}", key, MapConsts.SeparatorChar, name);
            }
            return key;
        }

        private static bool IsComplexType(Type objType)
        {
            return !TypeDescriptor.GetConverter(objType).CanConvertFrom(typeof(string));
        }

        #endregion
    }
}