using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Bussiness.Order.DataContract
{
    [DataContract]
    public class OrderModelResponse : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long? ProductId { get; set; }

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
