namespace MyDAL.Core.Enums
{
    /// <summary>
    /// sql 动作枚举
    /// </summary>
    internal enum ActionEnum
    {
        /// <summary>
        /// ""
        /// </summary>
        None,

        SQL,

        /// <summary>
        /// ""
        /// </summary>
        Insert,

        /// <summary>
        /// 
        /// </summary>
        Update,

        /// <summary>
        /// ""
        /// </summary>
        Select,

        /// <summary>
        /// ""
        /// </summary>
        From,

        /// <summary>
        /// " inner join "
        /// </summary>
        InnerJoin,

        /// <summary>
        /// " left join "
        /// </summary>
        LeftJoin,

        /// <summary>
        /// " on "
        /// </summary>
        On,

        /// <summary>
        /// " where "
        /// </summary>
        Where,

        /// <summary>
        /// " and "
        /// </summary>
        And,

        /// <summary>
        /// " or "
        /// </summary>
        Or,

        /// <summary>
        /// ""
        /// </summary>
        OrderBy
    }
}
