using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Ctrip.SOA.Infratructure.Exceptions
{
    /// <summary>
    /// 访问异常
    /// add by luzm
    /// </summary>
    [DataContract]
    [Serializable]
    public class PermissionException
    {
        public PermissionException()
        { }
        public PermissionException(String message)
        {
            this.Message = message;
        }
        public PermissionException(Exception ex)
        {
            this.Message = ex.Message;
        }
        [DataMember]
        public const int Code = 1000;

        [DataMember]
        public string Message { get; set; }

        /// <summary>
        ///  抛出错误
        /// </summary>
        public void Throw()
        {
            throw new FaultException<PermissionException>(this, new FaultReason(this.Message));
        }
    }
}
