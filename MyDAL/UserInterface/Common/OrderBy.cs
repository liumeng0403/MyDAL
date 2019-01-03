using System;

namespace MyDAL
{
    /// <summary>
    ///     排序设置
    /// </summary>
    public sealed class OrderBy
    {
        /// <summary>
        /// TableModel - 类型
        /// </summary>
        public Type Table { get; set; }

        /// <summary>
        ///     排序字段
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        ///     排序方向, 默认倒序
        /// </summary>
        public OrderByEnum Direction { get; set; } = OrderByEnum.Desc;
    }
}
