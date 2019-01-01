using System.Collections.Generic;

namespace MyDAL
{
    /// <summary>
    ///     分页查询设置
    /// </summary>
    public abstract class PagingQueryOption
    {
        /// <summary>
        ///     当前页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        ///     页面大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        ///     排序字段
        /// </summary>
        public List<OrderBy> OrderBys { get; set; } = new List<OrderBy>();
    }

}
