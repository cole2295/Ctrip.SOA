/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

using Ctrip.SOA.Repository.Product.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.Product.IRepository
{
    public interface IProductRepository
    {
        long AddProduct(ProductEntity entity);

        bool UpdateProduct(ProductEntity entity);

        void DeleteProduct(ProductEntity entity);

        ProductEntity GetProduct(long productId);

        List<ProductEntity> GetAllProduct();
    }
}


