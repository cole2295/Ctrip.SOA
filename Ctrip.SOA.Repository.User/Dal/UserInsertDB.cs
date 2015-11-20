using Ctrip.SOA.Repository.User.Interface;
using Ctrip.SOA.Repository.User.Model;
using Ctrip.SOA.Infratructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.User.Dal
{
    public class UserInsertDB : DALContext, IUserInsertDB
    {
        public UserInsertDB() : base(DBConsts.TestDB) { }

        public long InsertUser(UserEntity entity)
        {
            var dbCommand = DB.GetStoredProcCommand("spA_User_i");
            AddInParameter(dbCommand, entity, false);
            DB.AddOutParameter(dbCommand, "@UserId", DbType.Int64, 8);

            DB.ExecuteNonQuery(dbCommand);
            entity.UserId = DbHelper.ConvertTo<long>(DB.GetParameterValue(dbCommand, "@UserId"));
            return entity.UserId;
        }

        public bool UpdateUser(UserEntity entity)
        {
            var dbCommand = DB.GetStoredProcCommand("spA_User_u");
            AddInParameter(dbCommand, entity, true);
            return DbHelper.ConvertTo<int>(DB.ExecuteScalar(dbCommand)) == 0;
        }

        public void DeleteUser(UserEntity entity)
        {
            var command = DB.GetStoredProcCommand("spA_User_d");
            DB.AddInParameter(command, "@UserId", DbType.Int64, entity.UserId);
            DB.ExecuteNonQuery(command);
        }

        protected void AddInParameter(DbCommand command, UserEntity entity, bool containsPrimaryKey)
        {
            DB.AddInParameter(command, "@UserName", DbType.String, entity.UserName);
            DB.AddInParameter(command, "@CreateTime", DbType.DateTime, entity.CreateTime);
            DB.AddInParameter(command, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
           
            if (containsPrimaryKey)
            {
                DB.AddInParameter(command, "@UserId", DbType.Int64, entity.UserId);
            }
        }
    }
}
