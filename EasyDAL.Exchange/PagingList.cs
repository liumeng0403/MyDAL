using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange
{
    /// <summary>
    ///     分页列表
    /// </summary>
    public class PagingList<TEntity>
    {
        /// <summary>
        ///     当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     条目总数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        ///     页面总数
        /// </summary>
        public int TotalPage
        {
            get
            {
                var totalPage = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                {
                    ++totalPage;
                }
                return (int)totalPage;
            }
        }

        /// <summary>
        ///     数据
        /// </summary>
        public List<TEntity> Data { get; set; }
    }
}
