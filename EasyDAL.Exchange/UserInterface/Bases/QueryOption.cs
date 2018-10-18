using MyDAL.Core.Common;
using System.Collections.Generic;

namespace MyDAL
{
    public abstract class QueryOption:IQueryOption
    {
        /// <summary>
        ///     默认排序字段
        /// </summary>
        /// <value>
        ///     The order by.
        /// </value>
        public List<OrderBy> OrderBys { get; set; } = new List<OrderBy>();
    }
}
