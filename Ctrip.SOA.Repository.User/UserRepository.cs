using Ctrip.SOA.Repository.User.Model;
using Ctrip.SOA.Repository.User.Interface;
using Ctrip.SOA.Repository.User.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ctrip.SOA.Repository.User
{
    public class UserRepository: IUserRepository
    {
        private readonly IUserInsertDB userInsertDB;
        private readonly IUserSelectDB userSelectDB;
        public UserRepository(IUserInsertDB userInsertDB, IUserSelectDB userSelectDB)
        {
            this.userInsertDB = userInsertDB;
            this.userSelectDB = userSelectDB;
        }

        public long AddUser(UserEntity entity)
        {
            return userInsertDB.InsertUser(entity);
        }

        public bool UpdateUser(UserEntity entity)
        {
            return userInsertDB.UpdateUser(entity);
        }

        public void DeleteUser(UserEntity entity)
        {
            userInsertDB.DeleteUser(entity);
        }

        public UserEntity GetUser(long userId)
        {
            return userSelectDB.SelectUser(userId);
        }

        public List<UserEntity> GetAllUser()
        {
            return userSelectDB.SelectAllUsers();
        }
    }
}
