/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/

using Ctrip.SOA.Repository.Product.Model;
using Ctrip.SOA.Repository.Product.Interface;
using Ctrip.SOA.Repository.Product.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ctrip.SOA.Repository.Product
{
    public class ProductRepository: IProductRepository
    {
        private readonly IProductInsertDB productInsertDB;
        private readonly IProductSelectDB productSelectDB;
        public ProductRepository(IProductInsertDB productInsertDB, IProductSelectDB productSelectDB)
        {
            this.productInsertDB = productInsertDB;
            this.productSelectDB = productSelectDB;
        }

        public long AddProduct(ProductEntity entity)
        {
            return productInsertDB.InsertProduct(entity);
        }

        public bool UpdateProduct(ProductEntity entity)
        {
            return productInsertDB.UpdateProduct(entity);
        }

        public void DeleteProduct(ProductEntity entity)
        {
            productInsertDB.DeleteProduct(entity);
        }

        public ProductEntity GetProduct(long productId)
        {
            return productSelectDB.SelectProduct(productId);
        }

        public List<ProductEntity> GetAllProduct()
        {
            return productSelectDB.SelectAllProducts();
        }
    }
}


