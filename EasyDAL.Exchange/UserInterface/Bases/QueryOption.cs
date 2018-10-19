using MyDAL.Core.Common;
using System.Collections.Generic;

namespace MyDAL
{
    public abstract class QueryOption : IQueryOption
    {
        /// <summary>
        ///     默认排序字段
        /// </summary>
        /// <value>
        ///     The order by.
        /// </value>
        public List<OrderBy> OrderBys { get; set; } = new List<OrderBy>();
    }

    /// <summary>
    ///     分页查询设置
    /// </summary>
    public abstract class PagingQueryOption : QueryOption
    {
        /// <summary>
        ///     当前页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        ///     页面大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

}
