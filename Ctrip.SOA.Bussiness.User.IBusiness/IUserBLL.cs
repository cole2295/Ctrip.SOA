using Ctrip.SOA.Bussiness.User.DataContract;
using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Bussiness.User.IBusiness
{
    public interface IUserBLL
    {
        UserModelResponse AddUser(UserModelRequest entity);

        BaseResponse UpdateUser(UserModelRequest entity);

        BaseResponse DeleteUser(UserModelRequest entity);

        List<UserModelResponse> GetAllUsers();

        UserModelResponse GetUser(UserModelRequest entity);
    }
}
