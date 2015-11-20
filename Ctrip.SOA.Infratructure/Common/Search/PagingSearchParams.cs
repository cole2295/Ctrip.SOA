using System;
using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.Common.Search
{
    /// <summary>
    /// 分页搜索参数。
    /// </summary>
    [DataContract]
    [Serializable]
    public class PagingSearchParams
    {
        /// <summary>
        /// 每页条数。
        /// </summary>
        public int EntriesPerPage { get; set; }

        /// <summary>
        /// 页编号。
        /// </summary>
        public int PageNumber { get; set; }
    }
}