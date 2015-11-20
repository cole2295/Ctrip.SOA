using Ctrip.SOA.Repository.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.User.Interface
{
    public interface IUserInsertDB
    {
        long InsertUser(UserEntity entity);

        bool UpdateUser(UserEntity entity);

        void DeleteUser(UserEntity entity);
    }
}
