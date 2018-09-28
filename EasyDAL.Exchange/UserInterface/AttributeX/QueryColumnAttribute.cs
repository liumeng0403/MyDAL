using System;

namespace Yunyong.DataExchange
{
    public class QueryColumnAttribute : Attribute
    {

        /// <summary>
        /// Table 列名
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// 查询动作
        /// </summary>
        public CompareConditionEnum CompareCondition { get; set; } = CompareConditionEnum.Equal;

    }
}
