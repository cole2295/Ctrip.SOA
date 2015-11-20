using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ctrip.SOA.Infratructure.Data;
using System.Data;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Ctrip.SOA.Infratructure.Common.Search
{
    public enum SerarchType
    {
        Equal,
        NotEqual,
        GreaterThanOrEqual,
        LessThanOrEqual,
        GreaterThan,
        LessThan,
        Like,
        In
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class SearchCondition
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public SerarchType SearchType { get; set; }

        [DataMember]
        public DbType dbType { get; set; }

        public SearchCondition Clone()
        {
            return this.MemberwiseClone() as SearchCondition;
        }


    }

    public static class ListExtForSearchCondition
    {
        private static readonly string conditionFormat = " {0} {1} and";

        private static readonly string symbolFormat = " {0} {1}";

        private static readonly string positionReg = @"\{\$where\}";

        /// <summary>
        /// return where condition combine with param name
        /// add param to command
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DbCommand BuildWhereCondition(this IEnumerable<SearchCondition> conditions, DbCommand command, DALContext context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var condition in conditions)
            {
                if (sb.Length == 0)
                {
                    sb.Append(" where ");
                }
                sb.Append(string.Format(conditionFormat, condition.Name, GetConditionStringBySearchType(condition, command, context)));
            }
            var reg = new Regex(positionReg, RegexOptions.IgnoreCase);

            string strParam = sb.ToString().TrimEnd("and".ToCharArray());
            if (reg.IsMatch(command.CommandText))
            {
                command.CommandText = reg.Replace(command.CommandText, strParam);
            }
            else
            {
                command.CommandText = command.CommandText + strParam;
            }
            return command;
        }

        private static string GetConditionStringBySearchType(SearchCondition condition, DbCommand command, DALContext context)
        {
            var searchType = condition.SearchType;
            string name = condition.Name.Replace(".", "_");
            //add by wucui,用于&的情况
            name = name.Replace("&", "_");

            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();


            string columnName = "@" + name;

            values.Add(new KeyValuePair<string, object>(columnName, condition.Value));

            string symbol = "=";

            string format = symbolFormat;

            switch (searchType)
            {
                case SerarchType.GreaterThan:
                    symbol = ">";
                    break;
                case SerarchType.GreaterThanOrEqual:
                    symbol = ">=";
                    break;
                case SerarchType.LessThan:
                    symbol = "<";
                    break;
                case SerarchType.LessThanOrEqual:
                    symbol = "<=";
                    break;
                case SerarchType.Like:
                    symbol = "like";
                    format = "{0} '%'+{1}+'%'";
                    break;
                case SerarchType.NotEqual:
                    symbol = "<>";
                    break;
                case SerarchType.In:
                    symbol = "in";
                    values = new List<KeyValuePair<string, object>>();
                    var conditions = condition.Value.ToString().Split(',');
                    for (var i = 0; i < conditions.Length; i++)
                    {
                        var conObj = conditions[i];
                        var conName = columnName + i.ToString();
                        values.Add(new KeyValuePair<string, object>(conName, conObj));
                    }
                    format = "{0} ({1})";
                    break;
                case SerarchType.Equal:
                default:
                    break;
            }

            StringBuilder returnValue = new StringBuilder();

            foreach (var value in values)
            {
                var key = GetKey(value.Key, context, command);
                context.DB.AddInParameter(command, key, condition.dbType, value.Value);
                returnValue.Append(key + ",");
            }

            return string.Format(format, symbol, returnValue.ToString().Trim(','));
        }

        private static string GetKey(string key, DALContext context, DbCommand command)
        {
            try
            {
                if (command.Parameters[key] != null)
                {
                    string[] strs = key.Split('_');
                    string cIndex = strs[strs.Length - 1];
                    int index = 0;
                    int.TryParse(cIndex.ToString(), out index);
                    key = key.Replace("_" + index, "") + "_" + ++index;
                    return GetKey(key, context, command);
                }
                else
                {
                    return key;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return key;
            }
        }
    }
}
