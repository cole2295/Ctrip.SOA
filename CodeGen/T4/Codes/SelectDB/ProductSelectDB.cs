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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.Product.Dal
{
    public class ProductSelectDB : DALContext, IProductSelectDB
    {
        public ProductSelectDB():base(DBConsts.TestDB)
        { }

        public ProductEntity SelectProduct(long productId)
        {
            const string SQL = "SELECT * FROM [Product] WITH(NOLOCK) WHERE [ProductId] = @ProductId";
            var dbCommand = DB.GetSqlStringCommand(SQL);
            DB.AddInParameter(dbCommand, "@ProductId", DbType.Int64, productId);
            using (var reader = DB.ExecuteReader(dbCommand))
            {
                return DbHelper.ConvertToEntity<ProductEntity>(reader);
            }
        }

        public List<ProductEntity> SelectAllProducts()
        {
            const string SQL = "SELECT * FROM [Product] WITH(NOLOCK) ";
            var dbCommand = DB.GetSqlStringCommand(SQL);
            using (var reader = DB.ExecuteReader(dbCommand))
            {
                return DbHelper.ConvertToEntityList<ProductEntity>(reader);
            }
        }
    }
}



