using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ctrip.SOA.Bussiness.Order.DataContract
{
    [DataContract]
    public class OrderModelRequest
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

        [DataMember]
        public long? ProductId { get; set; }

    }
}
