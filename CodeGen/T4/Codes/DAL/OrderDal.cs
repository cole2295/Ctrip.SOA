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
    public class OrderDal : DalContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrderDal() : base(DbConsts.SkyseasPrdDb) { }
        
        /// <summary>
        /// 向数据库中插入 Order 实体
        /// </summary>
        /// <param name="entity">OrderModel对象</param>
        /// <returns>自增主键</returns>
        public long InsertOrder(OrderEntity entity)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddOutParameter("@OrderId", DbType.Int64, 8);
            parameters.AddInParameter("@UserId", DbType.Int64, entity.UserId);
            parameters.AddInParameter("@Name", DbType.String, entity.Name);
            parameters.AddInParameter("@Price", DbType.Decimal, entity.Price);
            parameters.AddInParameter("@ProductId", DbType.Int64, entity.ProductId);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, entity.CreateTime);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, entity.UpdateTime);
            
            try
            {
                DB.ExecSp("spA_Order_i", parameters);
                return Convert.ToInt64(parameters["@OrderId"].Value);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 InsertOrder 时出错", ex);
            }
        }

        /// <summary>
        /// 根据自增主键删除数据库中 Order 实体
        /// </summary>
        /// <param name="primaryKey">自增主键</param>
        public void DeleteOrderByPk(long primaryKey)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@OrderId", DbType.Int64, primaryKey);

            try
            {
                DB.ExecSp("spA_Order_d", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 DeleteOrderByPk  时出错", ex);
            }
        }
        
        /// <summary>
        /// 更新Order 实体
        /// </summary>
        /// <param name="entity">OrderModel对象</param>
        public void UpdateOrder(OrderEntity entity)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@OrderId", DbType.Int64, entity.OrderId);
            parameters.AddInParameter("@UserId", DbType.Int64, entity.UserId);
            parameters.AddInParameter("@Name", DbType.String, entity.Name);
            parameters.AddInParameter("@Price", DbType.Decimal, entity.Price);
            parameters.AddInParameter("@ProductId", DbType.Int64, entity.ProductId);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, entity.CreateTime);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, entity.UpdateTime);
            
            try
            {
                DB.ExecSp("spA_Order_u", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 UpdateOrder 时出错", ex);
            }
        }

        /// <summary>
        /// 更新时间戳
        /// </summary>
        public void UpdateDataChangeLastTime(long id)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@OrderId", DbType.Int64, id);
            parameters.AddInParameter("@DataChange_LastTime", DbType.DateTime, DBNull.Value);
            
            try
            {
                DB.ExecSp("spA_Order_u", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 UpdateDataChangeLastTime 时出错", ex);
            }
        }

        /// <summary>
        /// 根据自增主键获取OrderModel对象
        /// </summary>
        /// <param name="primaryKey">自增主键</param>
        /// <returns>OrderModel对象</returns>
        public OrderEntity GetOrderByPk(long primaryKey)
        {
            try
            {
                return DB.GetByKey<OrderEntity>(primaryKey);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 FindByPk 时出错", ex);
            }
        }

        /// <summary>
        /// 获取 Order 实体的查询对象
        /// </summary>
        public IQuery<OrderEntity> GetQuery()
        {
            try
            {
                return DB.GetQuery<OrderEntity>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 GetQuery 时出错", ex);
            }
        }

        /// <summary>
        /// 根据 Order 实体的查询对象，获取OrderModel对象
        /// </summary>
        /// <param name="query">Order 实体的查询对象</param>
        /// <returns>OrderModel对象</returns>
        public OrderEntity GetOrder(IQuery<OrderEntity> query)
        {
            try
            {
                return DB.SelectFirst<OrderEntity>(query);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问GetOrder时出错", ex);
            }
        }

        /// <summary>
        /// 根据 Order 实体的查询对象，获取OrderModel对象列表
        /// </summary>
        /// <param name="query">Order 实体的查询对象</param>
        /// <returns>OrderModel对象列表</returns>
        public IList<OrderEntity> GetOrderList(IQuery<OrderEntity> query)
        {
            try
            {
                return DB.SelectList<OrderEntity>(query);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问 GetList 时出错", ex);
            }
        }

        /// <summary>
        /// 获取所有OrderModel对象列表
        /// </summary>
        public IList<OrderEntity> GetAllOrderList()
        {
            try
            {
                return DB.GetAll<OrderEntity>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用 OrderDal 时，访问GetAllOrderList时出错", ex);
            }
        }
    }
}
