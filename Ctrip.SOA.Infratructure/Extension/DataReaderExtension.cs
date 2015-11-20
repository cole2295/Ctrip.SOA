using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Extension
{
    public static class DataReaderExtension
    {
        public static T GetValueOrDefault<T>(this IDataReader row, string fieldName)
        {
            try
            {
                int ordinal = row.GetOrdinal(fieldName);
                return row.GetValueOrDefault<T>(ordinal);
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetValueOrDefault<T>(this IDataReader row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }

        public static bool GetBoolValue(this IDataReader row, string fieldName)
        {
            try
            {
                int ordinal = row.GetOrdinal(fieldName);
                return row.GetValue(ordinal).ToString() == "T" ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public static bool GetBoolValueForNum(this IDataReader row, string fieldName)
        {
            try
            {
                int ordinal = row.GetOrdinal(fieldName);
                return row.GetValue(ordinal).ToString() == "True" ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}