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
    public class UserDal : DalContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserDal() : base(DbConsts.SkyseasPrdDb) { }
        
        /// <summary>
        /// 向数据库中插入 User 实体
        /// </summary>
        /// <param name="entity">UserModel对象</param>
        /// <returns>自增主键</returns>
        public long InsertUser(UserEntity entity)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddOutParameter("@UserId", DbType.Int64, 8);
            parameters.AddInParameter("@UserName", DbType.String, entity.UserName);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, entity.CreateTime);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, entity.UpdateTime);
            
            try
            {
                DB.ExecSp("spA_User_i", parameters);
                return Convert.ToInt64(parameters["@UserId"].Value);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 InsertUser 时出错", ex);
            }
        }

        /// <summary>
        /// 根据自增主键删除数据库中 User 实体
        /// </summary>
        /// <param name="primaryKey">自增主键</param>
        public void DeleteUserByPk(long primaryKey)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@UserId", DbType.Int64, primaryKey);

            try
            {
                DB.ExecSp("spA_User_d", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 DeleteUserByPk  时出错", ex);
            }
        }
        
        /// <summary>
        /// 更新User 实体
        /// </summary>
        /// <param name="entity">UserModel对象</param>
        public void UpdateUser(UserEntity entity)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@UserId", DbType.Int64, entity.UserId);
            parameters.AddInParameter("@UserName", DbType.String, entity.UserName);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, entity.CreateTime);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, entity.UpdateTime);
            
            try
            {
                DB.ExecSp("spA_User_u", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 UpdateUser 时出错", ex);
            }
        }

        /// <summary>
        /// 更新时间戳
        /// </summary>
        public void UpdateDataChangeLastTime(long id)
        {
            var parameters = new StatementParameterCollection();
            parameters.AddInParameter("@UserId", DbType.Int64, id);
            parameters.AddInParameter("@DataChange_LastTime", DbType.DateTime, DBNull.Value);
            
            try
            {
                DB.ExecSp("spA_User_u", parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 UpdateDataChangeLastTime 时出错", ex);
            }
        }

        /// <summary>
        /// 根据自增主键获取UserModel对象
        /// </summary>
        /// <param name="primaryKey">自增主键</param>
        /// <returns>UserModel对象</returns>
        public UserEntity GetUserByPk(long primaryKey)
        {
            try
            {
                return DB.GetByKey<UserEntity>(primaryKey);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 FindByPk 时出错", ex);
            }
        }

        /// <summary>
        /// 获取 User 实体的查询对象
        /// </summary>
        public IQuery<UserEntity> GetQuery()
        {
            try
            {
                return DB.GetQuery<UserEntity>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 GetQuery 时出错", ex);
            }
        }

        /// <summary>
        /// 根据 User 实体的查询对象，获取UserModel对象
        /// </summary>
        /// <param name="query">User 实体的查询对象</param>
        /// <returns>UserModel对象</returns>
        public UserEntity GetUser(IQuery<UserEntity> query)
        {
            try
            {
                return DB.SelectFirst<UserEntity>(query);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问GetUser时出错", ex);
            }
        }

        /// <summary>
        /// 根据 User 实体的查询对象，获取UserModel对象列表
        /// </summary>
        /// <param name="query">User 实体的查询对象</param>
        /// <returns>UserModel对象列表</returns>
        public IList<UserEntity> GetUserList(IQuery<UserEntity> query)
        {
            try
            {
                return DB.SelectList<UserEntity>(query);
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问 GetList 时出错", ex);
            }
        }

        /// <summary>
        /// 获取所有UserModel对象列表
        /// </summary>
        public IList<UserEntity> GetAllUserList()
        {
            try
            {
                return DB.GetAll<UserEntity>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用 UserDal 时，访问GetAllUserList时出错", ex);
            }
        }
    }
}
