using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// 使用DataRow作为实体的数据源
    /// </summary>
    public class DataRowDataSource : IRowDataSource
    {
        #region [ Fields ]

        private DataRow _dataRow;

        #endregion

        #region [ Constructors ]

        public DataRowDataSource(DataRow dataRow)
        {
            _dataRow = dataRow;
        }

        #endregion

        #region [ Properties ]

        public object this[string columnName]
        {
            get
            {
                return _dataRow[columnName];
            }
        }

        #endregion

        #region [ Methods ]

        public bool ContainsColumn(string columnName)
        {
            return _dataRow.Table.Columns.Contains(columnName);
        }        

        public IEnumerator<string> GetEnumerator()
        {
            return new DataRowColumnNameEnumerator(_dataRow);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DataRowColumnNameEnumerator(_dataRow); 
        }

        public void Dispose()
        {
        }

        #endregion
              

        #region [ Nested Class ]

        private class DataRowColumnNameEnumerator : IEnumerator<string>
        {
            #region [ Fields ]

            private IEnumerator _internalEnumeator;

            #endregion

            #region [ Constructors ]

            public DataRowColumnNameEnumerator(DataRow dr)
            {
                _internalEnumeator = dr.Table.Columns.GetEnumerator();
            }

            #endregion

            #region [ Properties ]

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            // Properties
            public string Current
            {
                get
                {
                    DataColumn column = _internalEnumeator.Current as DataColumn;
                    return column.ColumnName;
                }
            }

            #endregion

            #region [ Methods ]

            public bool MoveNext()
            {
                return _internalEnumeator.MoveNext();
            }

            public void Reset()
            {
                _internalEnumeator.Reset();
            }

            public void Dispose()
            {
            }

            #endregion                      
        }

        #endregion
    }
}