using System;
using System.Collections;
using System.Collections.Generic;

namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// 使用Dictionary作为实体的数据源
    /// </summary>
    public class DictionaryDataSource : IRowDataSource
    {
        #region [ Fields ]
        
        private IDictionary _dictionary;

        #endregion

        #region [ Constructors ]

        public DictionaryDataSource(IDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        #endregion

        #region [ Properties ]

        public object this[string columnName]
        {
            get
            {
                return _dictionary[columnName];
            }
        }

        #endregion

        #region [ Methods ]

        public bool ContainsColumn(string columnName)
        {
            return _dictionary.Contains(columnName);
        }

        public void Dispose()
        {
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new DictionaryKeyEnumerator(_dictionary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.Keys.GetEnumerator();
        }

        #endregion

        #region [ Nested Class ]

        private class DictionaryKeyEnumerator : IEnumerator<string>
        {
            #region [ Fields ]

            private IEnumerator _internalEnumeator;

            #endregion

            #region [ Constructors ]

            public DictionaryKeyEnumerator(IDictionary dic)
            {
                _internalEnumeator = dic.Keys.GetEnumerator();
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

            public string Current
            {
                get
                {
                    return _internalEnumeator.Current as string;
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