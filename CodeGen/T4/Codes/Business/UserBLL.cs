/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/

using Ctrip.SOA.Bussiness.User.DataContract;
using Ctrip.SOA.Bussiness.User.IBusiness;
using Ctrip.SOA.Repository.User.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.Utility;
using Ctrip.SOA.Infratructure.ServiceProxy;

namespace Ctrip.SOA.Bussiness.User
{
    public class UserBLL:IUserBLL
    {
        private readonly IUserRepository UserRepository;

        public UserBLL(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModelResponse AddUser(UserModelRequest entity)
        {
            try
            {
                var userEntity = entity.MapTo<UserEntity>();
               
                long id = userRepository.AddUser(userEntity);

                return id > 0 ? new UserModelResponse { UserId = id, IsSuccess = true } : new UserModelResponse { IsSuccess = false, UserId = 0 };
            }
            catch(Exception ex)
            {
                return new UserModelResponse { UserId = 0, IsSuccess = false, Msg = ex.Message };
            }

        }

        public BaseResponse UpdateUser(UserModelRequest entity)
        {
            try
            {
                var userEntity = entity.MapTo<UserEntity>();
                var usermodel = userRepository.GetUser(userEntity.UserId);

                if (usermodel == null)
                    return new BaseResponse { IsSuccess = false, Msg = "can not find mddel" };

             
                bool result = userRepository.UpdateUser(usermodel);

                return result ? new BaseResponse { IsSuccess = true } : new BaseResponse { IsSuccess = false};
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false, Msg = ex.Message };
            }
        }

        public BaseResponse DeleteUser(UserModelRequest entity)
        {
            try
            {
                var userEntity = entity.MapTo<UserEntity>();
                userRepository.DeleteUser(userEntity);

                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false, Msg = ex.Message };
            }
        }

        public List<UserModelResponse> GetAllUsers()
        {
            try
            {
                List<UserEntity> users = userRepository.GetAllUser();

                return users.MapToList<UserModelResponse>();
            }
            catch (Exception)
            {
                return null ;
            }
        }

        public UserModelResponse GetUser(UserModelRequest entity)
        {
            try
            {
                UserEntity user = userRepository.GetUser(entity.UserId);

                return user.MapTo<UserModelResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}


