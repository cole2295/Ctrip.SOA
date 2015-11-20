/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/


using Ctrip.SOA.Repository.User.Interface;
using Ctrip.SOA.Repository.User.Model;
using HHInfratructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.User.Dal
{
    public class UserSelectDB : DALContext, IUserSelectDB
    {
        public UserSelectDB():base(DBConsts.TestDB)
        { }

        public UserEntity SelectUser(long userId)
        {
            const string SQL = "SELECT * FROM [User] WITH(NOLOCK) WHERE [UserId] = @UserId";
            var dbCommand = DB.GetSqlStringCommand(SQL);
            DB.AddInParameter(dbCommand, "@UserId", DbType.Int64, userId);
            using (var reader = DB.ExecuteReader(dbCommand))
            {
                return DbHelper.ConvertToEntity<UserEntity>(reader);
            }
        }

        public List<UserEntity> SelectAllUsers()
        {
            const string SQL = "SELECT * FROM [User] WITH(NOLOCK) ";
            var dbCommand = DB.GetSqlStringCommand(SQL);
            using (var reader = DB.ExecuteReader(dbCommand))
            {
                return DbHelper.ConvertToEntityList<UserEntity>(reader);
            }
        }
    }
}



