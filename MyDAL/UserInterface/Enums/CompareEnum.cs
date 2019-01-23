namespace MyDAL
{
    /// <summary>
    /// 用于 Paging Option 以指定属性对应 column 的比较作用
    /// </summary>
    public enum CompareEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        None,

        /// <summary>
        /// = 【等于】
        /// </summary>
        Equal,

        /// <summary>
        /// != 【不等于】
        /// </summary>
        NotEqual,

        /// <summary>
        /// &lt; 【小于】
        /// </summary>
        LessThan,

        /// <summary>
        /// &lt;= 【小于等于】
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// &gt; 【大于】
        /// </summary>
        GreaterThan,

        /// <summary>
        /// &gt;= 【大于等于】
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// " like " 【SQL - 类似：like '%条件%'，若包含通配符，则按自定义字符串 like 匹配】
        /// </summary>
        Like,
        /// <summary>
        /// " like " 【SQL - 类似：like '%条件'】
        /// </summary>
        Like_StartsWith,
        /// <summary>
        /// " like " 【SQL - 类似：like '条件%'】
        /// </summary>
        Like_EndsWith,

        /// <summary>
        /// " in " 【SQL - in(para1,para2,para3, ... ... )】
        /// </summary>
        In,

        /// <summary>
        /// " not in " 【SQL- not in(para1,para2,para3, ... ... )】
        /// </summary>
        NotIn        
    }
}
