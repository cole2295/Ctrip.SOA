using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Transactions;
using Ctrip.SOA.Infratructure.Entity;
using Ctrip.SOA.Infratructure.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Ctrip.SOA.Infratructure.Data
{
    /// <summary>
    /// OR转换类，暂时不记录日志。
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// 执行 Transact-SQL 语句。
        /// </summary>
        /// <typeparam name="TReturn">返回值类型。</typeparam>
        /// <returns></returns>
        public static TReturn ExecuteScalar<TReturn>(object obj)
        {
            return (TReturn)Convert.ChangeType(obj, typeof(TReturn));
        }

        /// <summary>
        /// 执行类型转换,根据改名
        /// </summary>
        /// <returns></returns>
        public static TReturn ConvertTo<TReturn>(this object obj)
        {
            return (TReturn)Convert.ChangeType(obj, typeof(TReturn));
        }

        /// <summary>
        /// 执行 Transact-SQL 语句, 返回多行单列集合。
        /// </summary>
        /// <typeparam name="TValue">返回值类型。</typeparam>
        /// <returns>多行单列集合。</returns>
        public static List<TValue> ConvertToList<TValue>(IDataReader dataReader)
        {
            //DataLogManager.ProfileDataCommand(this);
            List<TValue> list = new List<TValue>();
            while (dataReader.Read())
            {
                TValue value = (TValue)Converter2.ToType(dataReader.GetValue(0), typeof(TValue));
                list.Add(value);
            }

            return list;
        }

        /// <summary>
        /// 执行 Transact-SQL 语句，返回对应实体。
        /// </summary>
        /// <typeparam name="T">表示返回实体类型。</typeparam>
        /// <returns>对应实体。</returns>
        public static T ConvertToEntity<T>(IDataReader dataReader) where T : class
        {
            //DataLogManager.ProfileDataCommand(this);

            MapResult<T> mapResult = null;

            if (dataReader.Read())
            {
                mapResult = DataMapper.Map<T>(dataReader);
            }

            //DataLogManager.ProfileEntityBuild(mapResult);

            return mapResult == null ? default(T) : mapResult.Entity;
        }

        /// <summary>
        /// 执行 Transact-SQL 语句，返回对应实体列表。
        /// </summary>
        /// <typeparam name="T">表示返回实体类型。</typeparam>
        /// <returns>对应实体列表。</returns>
        public static List<T> ConvertToEntityList<T>(IDataReader dataReader) where T : class
        {
            //DataLogManager.ProfileDataCommand(this);

            List<T> list = new List<T>();
            MapResult<T> mapResult = null;
            while (dataReader.Read())
            {
                mapResult = DataMapper.Map<T>(dataReader);

                if (mapResult == null)
                {
                    mapResult = new MapResult<T>(default(T));
                    //DataLogManager.ProfileEntityBuild(mapResult);
                }
                else if (mapResult.Entity != null)
                {
                    list.Add(mapResult.Entity);
                }
            }

            return list;
        }


        public static object GetPropertyMapInObject(PropertyMapCollection maps, object root, string propertyMapName)
        {
            object target = root;
            string[] strArray = propertyMapName.Split(new char[1]
              {
                '_'
              });
            if (strArray.Length > 1)
            {
                for (int index1 = 0; index1 < strArray.Length - 1; ++index1)
                {
                    string index2 = strArray[index1];
                    target = maps[index2].Property.GetValue(target);
                }
            }
            return target;
        }

        //todo:可考虑使用下面的方法替换上面相关类
        public static TResult ExecuteScalar2<TResult, TEntity>(Database DB, DbCommand dbCommand, TEntity entity) where TEntity : class
        {
            DbParameter[] dbParameterArr = GetDbParameters<TEntity>(entity);
            dbCommand.Parameters.AddRange(dbParameterArr);
            object result = DB.ExecuteScalar(dbCommand);
            if (result == DBNull.Value)
                return default(TResult);
            return (TResult)Convert.ChangeType(result, typeof(TResult));
        }

        public static List<T> ConvertToList2<T>(IDataReader dr) where T : class, new()
        {
            List<T> list = null;
            var properties = GetProperties<T>();
            var columnNames = GetColumnNames(dr);
            while (dr.Read())
            {
                if (list == null)
                {
                    list = new List<T>();
                }
                T o = new T();
                foreach (var p in properties)
                {
                    var columnName = p.Name;
                    if (columnNames.Contains(columnName))
                    {
                        if (!Convert.IsDBNull(dr[columnName]))
                        {
                            object value = dr[columnName];
                            if (value.GetType() != p.PropertyType)
                            {
                                value = Convert.ChangeType(value, p.PropertyType);
                            }
                            p.SetValue(o, value, null);
                        }
                    }
                }
                list.Add(o);
            }
            return list;
        }

        public static T ConvertToEntity2<T>(IDataReader dr) where T : class,new()
        {
            T o = new T();
            PropertyInfo[] properties = GetProperties<T>();
            var columnNames = GetColumnNames(dr);

            foreach (var p in properties)
            {
                var columnName = p.Name;
                if (columnNames.Contains(columnName))
                {
                    if (!Convert.IsDBNull(dr[columnName]))
                    {
                        object value = dr[columnName];
                        if (value.GetType() != p.PropertyType)
                        {
                            value = Convert.ChangeType(value, p.PropertyType);
                        }
                        p.SetValue(o, value, null);
                    }
                }
            }
            return o;
        }

        public static DbCommand BuildDynamicParam(string toReplaceString, string strSql, string strParam, char sperateSymbol, DbCommand dbCommand, DbType dbType, BaseDaoExt DB)
        {
            StringBuilder sb = new StringBuilder();
            string[] ids = strParam.Split(sperateSymbol);
            List<string> keys = new List<string>();

            for (var i = 0; i < ids.Length; i++)
            {
                string key = "@key" + i;
                keys.Add(key);
                sb.Append(key + ",");
            }
            strSql = strSql.Replace(toReplaceString, sb.ToString().TrimEnd(','));

            //DbCommand dbCommand = DB.GetSqlStringCommand(strSql);
            for (var i = 0; i < ids.Length; i++)
            {
                DB.AddInParameter(dbCommand, keys[i], dbType, ids[i]);
            }
            dbCommand.CommandText = strSql;
            return dbCommand;
        }

        private static PropertyInfo[] GetProperties<T>() where T : class
        {
            Type t = typeof(T);
            string key = string.Format("DbHelper_GetProperties_{0}", t.FullName);
            MemoryCache cache = MemoryCache.Default;
            if (cache[key] == null)
            {
                PropertyInfo[] properties = t.GetProperties();
                cache.Add(key, properties, MemoryCache.InfiniteAbsoluteExpiration);
                return properties;
            }
            return cache[key] as PropertyInfo[];
        }

        private static DbParameter[] GetDbParameters<T>(T entity) where T : class
        {
            IList<DbParameter> parameterList = new List<DbParameter>();
            PropertyInfo[] properties = GetProperties<T>();
            foreach (var p in properties)
            {
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = string.Format("@{0}", p.Name);
                sqlParameter.Value = p.GetValue(entity, null);
                sqlParameter.DbType = GetDbType(p.PropertyType);
                parameterList.Add(sqlParameter);
            }
            return parameterList.ToArray();
        }

        private static List<string> GetColumnNames(IDataReader dr)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                var fileName = dr.GetName(i);
                list.Add(fileName);
            }
            return list;
        }

        private static DbType GetDbType(Type t)
        {
            string typeName = t.Name.ToLower();
            DbType dbType;
            switch (typeName)
            {
                case "guid":
                    dbType = DbType.Guid;
                    break;
                case "short":
                    dbType = DbType.Int16;
                    break;
                case "byte":
                    dbType = DbType.Byte;
                    break;
                case "int":
                    dbType = DbType.Int32;
                    break;
                case "long":
                    dbType = DbType.Int64;
                    break;
                case "dateTime":
                    dbType = DbType.DateTime;
                    break;
                case "double":
                    dbType = DbType.Double;
                    break;
                case "decimal":
                    dbType = DbType.Decimal;
                    break;
                case "float":
                    dbType = DbType.Decimal;
                    break;
                case "bool":
                    dbType = DbType.Boolean;
                    break;
                default:
                    dbType = DbType.String;
                    break;
            }
            return dbType;
        }
    }
}