using System.Collections;
using System.Collections.Generic;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DictionaryGenericDataSource : IRowDataSource
    {
        #region [ Fields ]

        private IDictionary<string, object> _dictionary;

        #endregion

        #region [ Constructors ]

        public DictionaryGenericDataSource(IDictionary<string, object> dictionary)
        {
            this._dictionary = dictionary;
        }

        #endregion

        #region [ Properties ]

        public object this[string columnName]
        {
            get
            {
                object result = null;
                this._dictionary.TryGetValue(columnName, out result);
                return result;
            }
        }

        #endregion

        #region [ Methods ]

        public bool ContainsColumn(string columnName)
        {
            return this._dictionary.ContainsKey(columnName);
        }

        public void Dispose()
        {
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this._dictionary.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._dictionary.Keys.GetEnumerator();
        }

        #endregion
    }
}