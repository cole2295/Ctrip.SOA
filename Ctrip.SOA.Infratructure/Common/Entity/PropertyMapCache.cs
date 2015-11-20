using System;
using System.Collections.Generic;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class PropertyMapCache
    {
        // Fields
        private static Dictionary<Type, PropertyMapCollection> s_maps = new Dictionary<Type, PropertyMapCollection>();
        private static object s_SyncObj = new object();

        // Methods
        public static void AddMap(Type objType, PropertyMap map)
        {
            PropertyMapCollection maps = null;
            if (!s_maps.TryGetValue(objType, out maps))
            {
                lock (s_SyncObj)
                {
                    if (!s_maps.TryGetValue(objType, out maps))
                    {
                        maps = new PropertyMapCollection();
                        if (!maps.Contains(map.Key))
                        {
                            maps.Add(map);
                        }
                        s_maps.Add(objType, maps);
                    }
                }
            }
            else if (!maps.Contains(map.Key))
            {
                lock (s_SyncObj)
                {
                    if (!maps.Contains(map.Key))
                    {
                        maps.Add(map);
                    }
                }
            }
        }

        public static PropertyMapCollection GetMaps(Type objType)
        {
            PropertyMapCollection maps = null;
            s_maps.TryGetValue(objType, out maps);
            return maps;
        }
    }
}
