using System;
using System.Collections.Generic;

namespace Ctrip.SOA.Infratructure.Entity
{  
    /// <summary>
    /// 表示填充实体的数据源
    /// </summary>
    public interface IRowDataSource : IEnumerable<string>, IDisposable
    {
        // Methods
        bool ContainsColumn(string columnName);

        // Properties
        object this[string columnName] { get; }
    }
}