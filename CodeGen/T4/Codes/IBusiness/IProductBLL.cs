/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

using Ctrip.SOA.Bussiness.Product.DataContract;
using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Bussiness.Product.IBusiness
{
    public interface IProductBLL
    {
        ProductModelResponse AddProduct(ProductModelRequest entity);

        BaseResponse UpdateProduct(ProductModelRequest entity);

        BaseResponse DeleteProduct(ProductModelRequest entity);

        List<ProductModelResponse> GetAllProducts();

        ProductModelResponse GetProduct(ProductModelRequest entity);
    }
}

