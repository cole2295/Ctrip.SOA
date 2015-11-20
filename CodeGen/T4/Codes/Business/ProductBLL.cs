/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/

using Ctrip.SOA.Bussiness.Product.DataContract;
using Ctrip.SOA.Bussiness.Product.IBusiness;
using Ctrip.SOA.Repository.Product.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.Utility;
using Ctrip.SOA.Infratructure.ServiceProxy;

namespace Ctrip.SOA.Bussiness.Product
{
    public class ProductBLL:IProductBLL
    {
        private readonly IProductRepository ProductRepository;

        public ProductBLL(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductModelResponse AddProduct(ProductModelRequest entity)
        {
            try
            {
                var productEntity = entity.MapTo<ProductEntity>();
               
                long id = productRepository.AddProduct(productEntity);

                return id > 0 ? new ProductModelResponse { ProductId = id, IsSuccess = true } : new ProductModelResponse { IsSuccess = false, ProductId = 0 };
            }
            catch(Exception ex)
            {
                return new ProductModelResponse { ProductId = 0, IsSuccess = false, Msg = ex.Message };
            }

        }

        public BaseResponse UpdateProduct(ProductModelRequest entity)
        {
            try
            {
                var productEntity = entity.MapTo<ProductEntity>();
                var productmodel = productRepository.GetProduct(productEntity.ProductId);

                if (productmodel == null)
                    return new BaseResponse { IsSuccess = false, Msg = "can not find mddel" };

             
                bool result = productRepository.UpdateProduct(productmodel);

                return result ? new BaseResponse { IsSuccess = true } : new BaseResponse { IsSuccess = false};
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false, Msg = ex.Message };
            }
        }

        public BaseResponse DeleteProduct(ProductModelRequest entity)
        {
            try
            {
                var productEntity = entity.MapTo<ProductEntity>();
                productRepository.DeleteProduct(productEntity);

                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false, Msg = ex.Message };
            }
        }

        public List<ProductModelResponse> GetAllProducts()
        {
            try
            {
                List<ProductEntity> products = productRepository.GetAllProduct();

                return products.MapToList<ProductModelResponse>();
            }
            catch (Exception)
            {
                return null ;
            }
        }

        public ProductModelResponse GetProduct(ProductModelRequest entity)
        {
            try
            {
                ProductEntity product = productRepository.GetProduct(entity.ProductId);

                return product.MapTo<ProductModelResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}


