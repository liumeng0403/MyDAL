using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Attributes
{
    /// <summary>
    /// Defines a table name mapper for getting table names from types.
    /// </summary>
    public interface ITableNameMapper
    {
        /// <summary>
        /// 获取 db 表名
        /// </summary>
        string GetTableName(Type type);
    }
}
