using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Bussiness.User.DataContract
{
    [DataContract]
    public class UserModelResponse : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? UpdateTime { get; set; }
    }
}
