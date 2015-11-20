using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Arch.Data;
using Arch.Data.DbEngine;

namespace Ctrip.SOA.Infratructure.Data
{
    /// <summary>
    /// Dal Fx数据上下文操作基类
    /// </summary>
    public class DALContext : IDisposable
    {
        #region Fields

        private readonly string _connectionKey;
        private BaseDaoExt _baseDao;

        /// <summary>
        /// 基础数据层操作类
        /// </summary>
        public BaseDaoExt DB
        {
            get
            {
                return _baseDao ?? new BaseDaoExt(_connectionKey);
                // return _baseDao ?? BaseDaoFactory.CreateBaseDao(_connectionKey);
            }
        }

        #endregion Fields

        #region Ctor

        public DALContext(string connectionKey)
        {
            _connectionKey = connectionKey;
            _baseDao = new BaseDaoExt(_connectionKey);
            //_baseDao = BaseDaoFactory.CreateBaseDao(_connectionKey);
        }

        #endregion Ctor

        #region Impl

        public void Dispose()
        {
            _baseDao = null;
        }

        #endregion Impl
    }
}