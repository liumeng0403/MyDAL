using System;

namespace MyDAL
{
    public sealed class QueryColumnAttribute : Attribute
    {

        /// <summary>
        /// Table 列名
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// 查询动作
        /// </summary>
        public CompareEnum CompareCondition { get; }

        public QueryColumnAttribute(string columnName, CompareEnum compareType = CompareEnum.Equal)
        {
            ColumnName = columnName;
            CompareCondition = compareType;
        }

    }
}
