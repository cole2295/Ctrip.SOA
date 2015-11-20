using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataMapper
    {
        public static IDataMapperProvider Provider = new DataMapperProvider();

        public static MapResult<TEntity> Map<TEntity>(IRowDataSource dataSource)
        {
            return Provider.Map<TEntity>(dataSource);
        }

        public static MapResult<TEntity> Map<TEntity>(Dictionary<string, object> dic)
        {
            DictionaryGenericDataSource ds = new DictionaryGenericDataSource(dic);
            return Map<TEntity>(ds);
        }

        public static MapResult<TEntity> Map<TEntity>(Hashtable ht)
        {
            DictionaryDataSource ds = new DictionaryDataSource(ht);
            return Map<TEntity>(ds);
        }

        public static MapResult<TEntity> Map<TEntity>(DataRow dr)
        {
            DataRowDataSource ds = new DataRowDataSource(dr);
            return Map<TEntity>(ds);
        }

        public static MapResult<TEntity> Map<TEntity>(IDataReader dr)
        {
            DataReaderDataSource ds = new DataReaderDataSource(dr);
            return Map<TEntity>(ds);
        }
    }
}