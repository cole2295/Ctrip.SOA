using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Arch.Data;
using Arch.Data.DbEngine;
using System.Collections.Specialized;

namespace Ctrip.SOA.Infratructure.Data
{
    /// <summary>
    /// BaseDao的扩展类对应的方法 ，以便 快速切换老版本（DataBase)代码
    /// add by luzm 2014-6-30
    /// </summary>
    public class BaseDaoExt : BaseDao
    {
        public BaseDaoExt(string dbKey)
            : base(dbKey)
        { }

        //StatementParameterCollection _ParmsList;

        //StatementParameterCollection ParmsList
        //{
        //    get
        //    {
        //        if (_ParmsList == null)
        //            _ParmsList = new StatementParameterCollection();
        //        return _ParmsList;
        //    }
        //}

        DbProviderFactory _dbProviderFactory;

        public DbProviderFactory DbProviderFactory
        {
            get
            {
                if (_dbProviderFactory == null)
                    _dbProviderFactory = new DbProviderFactory();
                ClearParms();
                return _dbProviderFactory;
            }
        }

        public DbCommand GetSqlStringCommand(string sql)
        {
            DbCommand dbCmd = this.DbProviderFactory.CreateCommand();
            dbCmd.CommandText = sql;
            dbCmd.CommandType = CommandType.Text;
            ClearParms(dbCmd);
            return dbCmd;
        }

        public DbCommand GetStoredProcCommand(string spName)
        {
            DbCommand dbCmd = this.DbProviderFactory.CreateCommand();
            dbCmd.CommandText = spName;
            dbCmd.CommandType = CommandType.StoredProcedure;
            ClearParms(dbCmd);
            return dbCmd;
        }

        public void AddInParameter(DbCommand dbCmd, string parmName, DbType dbType, object value = null)
        {
            CreateParameter(dbCmd, parmName, dbType, ParameterDirection.Input, value);
            //AddInParameter(parmName, dbType, value);
        }

        /// <summary>
        ///  重写AddInParameter，有个bug处理
        /// </summary>
        /// <param name="parmName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        private void AddInParameter(string parmName, DbType dbType, object value)
        {
            //if (dbType == DbType.Boolean)       //必须这样写，一个框架的bug
            //    ParmsList.AddInParameter(parmName, DbType.Boolean, value, false);
            //else
            //    ParmsList.AddInParameter(parmName, dbType, value);
        }

        private void CreateParameter(DbCommand dbCmd, string parmName, DbType dbType, ParameterDirection direction, object value, int size = 0)
        {
            DbParameter parm = dbCmd.CreateParameter();
            parm.ParameterName = parmName;
            parm.DbType = dbType;
            parm.Value = value;
            if (size > 0)
                parm.Size = size;
            parm.Direction = direction;
            dbCmd.Parameters.Add(parm);
        }

        private StatementParameterCollection ChangeStateParm(DbCommand dbCmd)
        {
            DbParameterCollection parmsList = dbCmd.Parameters;
            StatementParameterCollection sparmList = new StatementParameterCollection();
            foreach (DbParameter dbParm in parmsList)
            {
                StatementParameter stateParm = new StatementParameter();
                stateParm.Name = dbParm.ParameterName;
                stateParm.DbType = dbParm.DbType;
                stateParm.Direction = dbParm.Direction;

                stateParm.Size = dbParm.Size;
                stateParm.Value = dbParm.Value;
                sparmList.Add(stateParm);
                // ParmsList.Add(stateParm);
            }
            return sparmList;
        }

        private void SetOutDbParameter(DbCommand dbCmd, StatementParameterCollection parms)
        {
            var outList = parms.Where(s => s.Direction == ParameterDirection.Output || s.Direction == ParameterDirection.InputOutput);
            if (outList.Count() == 0)  //若没有Output参数，则退出
                return;

            var dbParmList = dbCmd.Parameters.Cast<DbParameter>();
            outList.ToList().ForEach(delegate(StatementParameter parm)
            {
                DbParameter dbParm = dbParmList.FirstOrDefault(s => s.ParameterName == parm.Name);
                dbParm.Value = parm.Value;
            });
        }

        public void AddOutParameter(DbCommand dbCmd, string parmName, DbType dbType, int size = 0)
        {
            CreateParameter(dbCmd, parmName, dbType, ParameterDirection.Output, null, size);
            //if (size == 0)  //若空，则代表不传
            //    ParmsList.AddOutParameter(parmName, dbType);
            //else
            //    ParmsList.AddOutParameter(parmName, dbType, size);
        }

        public object GetParameterValue(DbCommand dbCmd, string parmName)
        {
            object value = null;
            var parmList = dbCmd.Parameters.Cast<DbParameter>().ToList();
            // if (ParmsList.Count > 0 && (ParmsList.Contains(parmName) || ParmsList.Contains(parmName.Replace("@", ""))))
            if (parmList.Count > 0 && (parmList.Exists(s => s.ParameterName == parmName || s.ParameterName == parmName.Replace("@", ""))))
            {
                if (parmList.Exists(s => s.ParameterName == parmName)) //(parmList.Contains(parmName))
                    value = parmList.First(s => s.ParameterName == parmName).Value;
                else
                    value = parmList.First(s => s.ParameterName == parmName.Replace("@", "")).Value;
            }
            return value;
        }

        public object ExecuteScalar(DbCommand command)
        {
            StatementParameterCollection parmList = ChangeStateParm(command);
            string sql = command.CommandText;
            object value;
            if (command.CommandType == CommandType.StoredProcedure)
                value = base.ExecScalarBySp(sql, parmList);
            else
                value = base.ExecScalar(sql, parmList);
            SetOutDbParameter(command, parmList);
            return value;
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            StatementParameterCollection parmList = ChangeStateParm(command);
            string sql = command.CommandText;
            IDataReader reader;
            if (command.CommandType == CommandType.StoredProcedure)
                reader = base.ExecDataReaderBySp(sql, parmList);
            else
                reader = base.SelectDataReader(sql, parmList);
            SetOutDbParameter(command, parmList);
            return reader;
        }

        public DataSet ExecuteDataSet(DbCommand command)
        {
            StatementParameterCollection parmList = ChangeStateParm(command);
            string sql = command.CommandText;
            DataSet ds;
            if (command.CommandType == CommandType.StoredProcedure)
                ds = base.ExecDataSetBySp(sql, parmList);
            else
                ds = base.SelectDataSet(sql, parmList);
            SetOutDbParameter(command, parmList);
            return ds;
        }

        public int ExecuteNonQuery(DbCommand command)
        {
         

            StatementParameterCollection parmList = ChangeStateParm(command);
            string sql = command.CommandText;
            int ret = 0;
            if (command.CommandType == CommandType.StoredProcedure)
            {
                var ddd = this;
                base.ExecSp(sql, parmList);
                //ret = (int)parmList["@return"].Value;
            }
            else
                ret = base.ExecNonQuery(sql, parmList);
            SetOutDbParameter(command, parmList);
            return ret;
        }

        public void ClearParms(DbCommand command = null)
        {
            if (command != null && command.Parameters.Count > 0)
                command.Parameters.Clear();
            //if (ParmsList.Count>0)
            //    ParmsList.Clear();
        }
    }

    /// <summary>
    ///  模板企业库(Basebase)中，扩展的类
    /// </summary>
    public class DbProviderFactory
    {
        public DbProviderFactory()
        { }

        public DbCommand CreateCommand()
        {
            DbConnection dbConn = new SqlConnection();
            DbCommand dbCmd = dbConn.CreateCommand();

            return dbCmd;
        }
    }
}