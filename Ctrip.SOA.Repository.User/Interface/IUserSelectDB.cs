using Ctrip.SOA.Repository.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.User.Interface
{
    public interface IUserSelectDB
    {
        /// <summary>
        /// 获得数据实体。
        /// </summary>
        /// <param name="orderId">数据实体的Id。</param>
        /// <returns>数据实体信息。</returns>
        UserEntity SelectUser(long userId);

        List<UserEntity> SelectAllUsers();
    }
}
