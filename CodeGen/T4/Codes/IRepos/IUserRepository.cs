/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

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


