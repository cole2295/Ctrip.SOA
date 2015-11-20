using Ctrip.SOA.Repository.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.User.IRepository
{
    public interface IUserRepository
    {
        long AddUser(UserEntity entity);

        bool UpdateUser(UserEntity entity);

        void DeleteUser(UserEntity entity);

        UserEntity GetUser(long userId);

        List<UserEntity> GetAllUser();
    }
}
