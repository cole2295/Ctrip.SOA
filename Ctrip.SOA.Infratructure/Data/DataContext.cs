using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ctrip.SOA.Infratructure.Data
{
    /// <summary>
    /// 数据库操作基类
    /// </summary>
    public class DataContext : IDisposable
    {
        #region Private Fields

        /// <summary>
        /// 数据库链接串
        /// </summary>
        private string connectionKey;

        /// <summary>
        /// 数据库私有成员
        /// </summary>
        private Database db;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets 数据库对象
        /// </summary>
        internal protected Database DB
        {
            get { return this.SetDatabase(); }
        }
        #endregion

        #region Private Functions

        /// <summary>
        /// 创建数据库对象
        /// </summary>
        /// <returns>数据库对象</returns>
        private Database SetDatabase()
        {
            if (this.db == null)
            {
                //old method to create db
                //this.db = EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionKey);                

                //new method to create db
                this.db = DatabaseFactory.CreateDatabase(connectionKey);
            }

            return this.db;
        }

        #endregion

        #region constructors

        /// <summary>
        /// 数据库基类构造函数
        /// </summary>
        /// <param name="connectionKey">数据库链接串</param>
        public DataContext(string connectionKey)
        {
            this.connectionKey = connectionKey;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// 释放数据库对象
        /// </summary>
        public void Dispose()
        {
            this.db = null;
        }

        #endregion
    }
}

