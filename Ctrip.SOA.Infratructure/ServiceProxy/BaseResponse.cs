using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; }
    }
}
