using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.Common.Search
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class PagingSearchResultBase<T>
    {
        [DataMember]
        public List<T> PageList { get; set; }

        [DataMember]
        public int TotalItemCount { get; set; }
    }
}
