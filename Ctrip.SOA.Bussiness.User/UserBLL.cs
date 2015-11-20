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
using Ctrip.SOA.Repository.User.Model;
using Ctrip.SOA.Infratructure.Aop;

namespace Ctrip.SOA.Bussiness.User
{
   
    public class UserBLL:IUserBLL
    {
        private readonly IUserRepository userRepository;

        public UserBLL(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
          
        }

 
        public UserModelResponse AddUser(UserModelRequest entity)
        {
            var userEntity = entity.MapTo<UserEntity>();
            userEntity.CreateTime = DateTime.Now;
            userEntity.UpdateTime = DateTime.Now;

            long id = userRepository.AddUser(userEntity);

            return id > 0 ? new UserModelResponse { UserId = id, IsSuccess = true } : new UserModelResponse { IsSuccess = false, UserId = 0 };

        }

        public BaseResponse UpdateUser(UserModelRequest entity)
        {
            var userEntity = entity.MapTo<UserEntity>();
            var usermodel = userRepository.GetUser(userEntity.UserId);

            if (usermodel == null)
                return new BaseResponse { IsSuccess = false, Msg = "can not find user" };

            usermodel.UpdateTime = DateTime.Now;
            usermodel.UserName = entity.UserName;
            bool result = userRepository.UpdateUser(usermodel);

            return result ? new BaseResponse { IsSuccess = true } : new BaseResponse { IsSuccess = false };
        }

        public BaseResponse DeleteUser(UserModelRequest entity)
        {
            var userEntity = entity.MapTo<UserEntity>();
            userRepository.DeleteUser(userEntity);

            return new BaseResponse { IsSuccess = true };
        }

        public List<UserModelResponse> GetAllUsers()
        {
            List<UserEntity> users = userRepository.GetAllUser();

            return users.MapToList<UserModelResponse>();
        }

        public UserModelResponse GetUser(UserModelRequest entity)
        {
            UserEntity user = userRepository.GetUser(entity.UserId);

            return user.MapTo<UserModelResponse>();
        }
    }
}
