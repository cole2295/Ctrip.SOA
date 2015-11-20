using Ctrip.SOA.Bussiness.User.DataContract;
using Ctrip.SOA.Bussiness.User.IBusiness;
using Ctrip.SOA.Bussiness.User.Service;
using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Ctrip.SOA.Infratructure.Aop;
using Ctrip.SOA.Infratructure.Unity;
using Ctrip.SOA.Infratructure.Wcf;

namespace Ctrip.SOA.Service.User
{
    [GlobalExceptionHandlerBehaviour(typeof(GlobalExceptionHandler))]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : IUserService
    {
        private IUserBLL userBLL;
        public UserService(IUserBLL userBLL)
        {
            this.userBLL = userBLL;
        }

        [LogTrace("AddUser-Trace")]
        [LogException("AddUser error")]
        public UserModelResponse AddUser(UserModelRequest entity)
        {
            //return new UserModelResponse { IsSuccess = true };
            return userBLL.AddUser(entity);
        }

        [Trancation]
        public BaseResponse UpdateUser(UserModelRequest entity)
        {
           
            var result = userBLL.UpdateUser(entity);
           

            return result;
        }
   
        public BaseResponse DeleteUser(UserModelRequest entity)
        {
            return userBLL.DeleteUser(entity);
        }

        [MetricElapsed("GetAllUsers")]
        public List<UserModelResponse> GetAllUsers()
        {
            return userBLL.GetAllUsers();
        }

        
        public UserModelResponse GetUser(UserModelRequest entity)
        {
            return userBLL.GetUser(entity);
        }
    }
}
