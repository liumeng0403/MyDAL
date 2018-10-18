using System.Collections.Generic;
using Yunyong.Core;

namespace Yunyong.DataExchange
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
