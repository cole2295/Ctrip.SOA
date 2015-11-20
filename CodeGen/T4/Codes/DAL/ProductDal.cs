/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

using System;
using System.Collections.Generic;
using System.Data;
using Arch.Data;
using Arch.Data.DbEngine;
using Arch.Data.Orm;
using BasicComponents.Consts;
using BasicComponents.Data;
using Entity.ProductDB.Entities;

namespace Ctrip.SOA.Repository.Order.DAL
{
    public class ProductDal : DalContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductDal() : base(DbConsts.SkyseasPrdDb) { }
        
        /// <summary>
        /// 向数据库中插入 Product 实体
        /// </summary>
        /// <param name="entity">ProductModel对象</param>
        /// <returns>自增主键</returns>
        public long InsertProduct(ProductEntity entity)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddOutParameter("@ProductId", DbType.Int64, 8);
            parameters.AddInParameter("@ProductName", DbType.String, entity.ProductName);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, entity.CreateTime);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, entity.UpdateTime);
            
            try
            {
                DB.ExecSp("spA_Product_i", parameters);
                return Convert.ToInt64(parameters["@ProductId"].Value);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 InsertProduct 时出错", ex);
            }
        }

        /// <summary>
        /// 根据自增主键删除数据库中 Product 实体
        /// </summary>
        /// <param name="primaryKey">自增主键</param>
        public void DeleteProductByPk(long primaryKey)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@ProductId", DbType.Int64, primaryKey);

            try
            {
                DB.ExecSp("spA_Product_d", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 DeleteProductByPk  时出错", ex);
            }
        }
        
        /// <summary>
        /// 更新Product 实体
        /// </summary>
        /// <param name="entity">ProductModel对象</param>
        public void UpdateProduct(ProductEntity entity)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@ProductId", DbType.Int64, entity.ProductId);
            parameters.AddInParameter("@ProductName", DbType.String, entity.ProductName);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, entity.CreateTime);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, entity.UpdateTime);
            
            try
            {
                DB.ExecSp("spA_Product_u", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 UpdateProduct 时出错", ex);
            }
        }

        /// <summary>
        /// 更新时间戳
        /// </summary>
        public void UpdateDataChangeLastTime(long id)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@ProductId", DbType.Int64, id);
            parameters.AddInParameter("@DataChange_LastTime", DbType.DateTime, DBNull.Value);
            
            try
            {
                DB.ExecSp("spA_Product_u", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 UpdateDataChangeLastTime 时出错", ex);
            }
        }

        /// <summary>
        /// 根据自增主键获取ProductModel对象
        /// </summary>
        /// <param name="primaryKey">自增主键</param>
        /// <returns>ProductModel对象</returns>
        public ProductEntity GetProductByPk(long primaryKey)
        {
            try
            {
                return DB.GetByKey<ProductEntity>(primaryKey);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 FindByPk 时出错", ex);
            }
        }

        /// <summary>
        /// 获取 Product 实体的查询对象
        /// </summary>
        public IQuery<ProductEntity> GetQuery()
        {
            try
            {
                return DB.GetQuery<ProductEntity>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 GetQuery 时出错", ex);
            }
        }

        /// <summary>
        /// 根据 Product 实体的查询对象，获取ProductModel对象
        /// </summary>
        /// <param name="query">Product 实体的查询对象</param>
        /// <returns>ProductModel对象</returns>
        public ProductEntity GetProduct(IQuery<ProductEntity> query)
        {
            try
            {
                return DB.SelectFirst<ProductEntity>(query);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问GetProduct时出错", ex);
            }
        }

        /// <summary>
        /// 根据 Product 实体的查询对象，获取ProductModel对象列表
        /// </summary>
        /// <param name="query">Product 实体的查询对象</param>
        /// <returns>ProductModel对象列表</returns>
        public IList<ProductEntity> GetProductList(IQuery<ProductEntity> query)
        {
            try
            {
                return DB.SelectList<ProductEntity>(query);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问 GetList 时出错", ex);
            }
        }

        /// <summary>
        /// 获取所有ProductModel对象列表
        /// </summary>
        public IList<ProductEntity> GetAllProductList()
        {
            try
            {
                return DB.GetAll<ProductEntity>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用 ProductDal 时，访问GetAllProductList时出错", ex);
            }
        }
    }
}
