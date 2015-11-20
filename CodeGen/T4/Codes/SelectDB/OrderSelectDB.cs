/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/


using Ctrip.SOA.Repository.Order.Interface;
using Ctrip.SOA.Repository.Order.Model;
using HHInfratructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.Order.Dal
{
    public class OrderSelectDB : DALContext, IOrderSelectDB
    {
        public OrderSelectDB():base(DBConsts.TestDB)
        { }

        public OrderEntity SelectOrder(long orderId)
        {
            const string SQL = "SELECT * FROM [Order] WITH(NOLOCK) WHERE [OrderId] = @OrderId";
            var dbCommand = DB.GetSqlStringCommand(SQL);
            DB.AddInParameter(dbCommand, "@OrderId", DbType.Int64, orderId);
            using (var reader = DB.ExecuteReader(dbCommand))
            {
                return DbHelper.ConvertToEntity<OrderEntity>(reader);
            }
        }

        public List<OrderEntity> SelectAllOrders()
        {
            const string SQL = "SELECT * FROM [Order] WITH(NOLOCK) ";
            var dbCommand = DB.GetSqlStringCommand(SQL);
            using (var reader = DB.ExecuteReader(dbCommand))
            {
                return DbHelper.ConvertToEntityList<OrderEntity>(reader);
            }
        }
    }
}



