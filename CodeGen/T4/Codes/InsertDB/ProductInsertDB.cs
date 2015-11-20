/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/

using Ctrip.SOA.Repository.Product.Interface;
using Ctrip.SOA.Repository.Product.Model;
using HHInfratructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.Product.Dal
{
    public class ProductInsertDB : DALContext, IProductInsertDB
    {
        public ProductInsertDB() : base(DBConsts.TestDB) { }

        public long InsertProduct(ProductEntity entity)
        {
            var dbCommand = DB.GetStoredProcCommand("spA_Product_i");
            AddInParameter(dbCommand, entity, false);
            DB.AddOutParameter(dbCommand, "@ProductId", DbType.Int64, 8);

            DB.ExecuteNonQuery(dbCommand);
            entity.ProductId = DbHelper.ConvertTo<long>(DB.GetParameterValue(dbCommand, "@ProductId"));
            return entity.ProductId;
        }

        public bool UpdateProduct(ProductEntity entity)
        {
            var dbCommand = DB.GetStoredProcCommand("spA_Product_u");
            AddInParameter(dbCommand, entity, true);
            return DbHelper.ConvertTo<int>(DB.ExecuteScalar(dbCommand)) == 0;
        }

        public void DeleteProduct(ProductEntity entity)
        {
            var command = DB.GetStoredProcCommand("spA_Product_d");
            DB.AddInParameter(command, "@ProductId", DbType.Int64, entity.ProductId);
            DB.ExecuteNonQuery(command);
        }

        protected void AddInParameter(DbCommand command, ProductEntity entity, bool containsPrimaryKey)
        {
		   			     						     
				    DB.AddInParameter(command, "@ProductName", DbType.String, entity.ProductName);
				 						     
				    DB.AddInParameter(command, "@CreateTime", DbType.DateTime, entity.CreateTime);
				 						     
				    DB.AddInParameter(command, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
				 			 
            if (containsPrimaryKey)
            {
                DB.AddInParameter(command, "@ProductId", DbType.Int64, entity.ProductId);
            }
        }
    }
}



