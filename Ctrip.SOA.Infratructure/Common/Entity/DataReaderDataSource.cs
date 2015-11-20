using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Ctrip.SOA.Infratructure.Entity
{    
    /// <summary>
    /// 使用DataReader作为实体的数据源
    /// </summary>
    public class DataReaderDataSource : IRowDataSource
    {
        #region [ Fields ]

        private IDataReader _dataReader;

        #endregion

        #region [ Constructors ]

        public DataReaderDataSource(IDataReader dr)
        {
            _dataReader = dr;
        }

        #endregion

        #region [ Properties ]

        public object this[string propertyName]
        {
            get
            {
                return this._dataReader[propertyName];
            }
        }

        #endregion

        #region [ Methods ]

        public bool ContainsColumn(string columnName)
        {
            return _dataReader.GetSchemaTable().Rows.Contains(columnName);
        }

        public void Dispose()
        {
            if (_dataReader != null)
            {
                _dataReader.Dispose();
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new DataReaderColumnNameEnumerator(_dataReader);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)this).GetEnumerator(); 
        }

        #endregion

        #region [ Nested Class ]

        /// <summary>
        /// 表示 <see cref="IDataReader"/> 对应列名的枚举。
        /// </summary>
        private class DataReaderColumnNameEnumerator : IEnumerator<string>
        {
            #region [ Fields ]

            private IEnumerator _internalEnumerator;

            #endregion

            #region [ Constructors ]

            /// <summary>
            /// 初始化 <see cref="DataReaderColumnNameEnumerator"/> 的实例。
            /// </summary>
            /// <param name="dr"></param>
            public DataReaderColumnNameEnumerator(IDataReader dr)
            {
                DataTable schemaTable = dr.GetSchemaTable();
                _internalEnumerator = schemaTable.Rows.GetEnumerator();
            }

            #endregion

            #region [ Properties ]

            /// <summary>
            /// 
            /// </summary>
            public string Current
            {
                get
                {
                    DataRow row = this._internalEnumerator.Current as DataRow;
                    return row["ColumnName"] as string;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            object IEnumerator.Current
            {
                get { return Current; }
            }

            #endregion

            #region [ Methods ]

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                return _internalEnumerator.MoveNext();
            }

            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                _internalEnumerator.Reset();
            }

            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                return;
            }

            #endregion
        }

        #endregion
    }
}