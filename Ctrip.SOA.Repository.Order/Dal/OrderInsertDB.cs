/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:01:46 
*/

using Ctrip.SOA.Repository.Order.Interface;
using Ctrip.SOA.Repository.Order.Model;
using Ctrip.SOA.Infratructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.Order.Dal
{
    public class OrderInsertDB : DALContext, IOrderInsertDB
    {
        public OrderInsertDB() : base(DBConsts.TestDB) { }

        public long InsertOrder(OrderEntity entity)
        {
            var dbCommand = DB.GetStoredProcCommand("spA_Order_i");
            AddInParameter(dbCommand, entity, false);
            DB.AddOutParameter(dbCommand, "@OrderId", DbType.Int64, 8);

            DB.ExecuteNonQuery(dbCommand);
            entity.OrderId = DbHelper.ConvertTo<long>(DB.GetParameterValue(dbCommand, "@OrderId"));
            return entity.OrderId;
        }

        public bool UpdateOrder(OrderEntity entity)
        {
            var dbCommand = DB.GetStoredProcCommand("spA_Order_u");
            AddInParameter(dbCommand, entity, true);
            return DbHelper.ConvertTo<int>(DB.ExecuteScalar(dbCommand)) == 0;
        }

        public void DeleteOrder(OrderEntity entity)
        {
            var command = DB.GetStoredProcCommand("spA_Order_d");
            DB.AddInParameter(command, "@OrderId", DbType.Int64, entity.OrderId);
            DB.ExecuteNonQuery(command);
        }

        protected void AddInParameter(DbCommand command, OrderEntity entity, bool containsPrimaryKey)
        {
		   			     						     
				    DB.AddInParameter(command, "@UserId", DbType.Int64, entity.UserId);
				 						     
				    DB.AddInParameter(command, "@Name", DbType.String, entity.Name);
				 						     
				    DB.AddInParameter(command, "@Price", DbType.Decimal, entity.Price);
				 						     
				    DB.AddInParameter(command, "@ProductId", DbType.Int64, entity.ProductId);
				 						     
				    DB.AddInParameter(command, "@CreateTime", DbType.DateTime, entity.CreateTime);
				 						     
				    DB.AddInParameter(command, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
				 			 
            if (containsPrimaryKey)
            {
                DB.AddInParameter(command, "@OrderId", DbType.Int64, entity.OrderId);
            }
        }
    }
}



