using Ctrip.SOA.Bussiness.User.DataContract;
using Ctrip.SOA.Infratructure.Aop;
using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Bussiness.User.Service
{
    [ServiceContract]
    public interface IUserService
    {
        
        [OperationContract]
        UserModelResponse AddUser(UserModelRequest entity);
        [OperationContract]
        BaseResponse UpdateUser(UserModelRequest entity);
        [OperationContract]
        BaseResponse DeleteUser(UserModelRequest entity);
        [OperationContract]
        List<UserModelResponse> GetAllUsers();
        [OperationContract]
        UserModelResponse GetUser(UserModelRequest entity);
    }
}
