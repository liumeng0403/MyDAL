namespace EasyDAL.Exchange
{
    public enum KeyTypeEnum
    {
        None,

        /// <summary>
        /// 普通索引
        /// </summary>
        Index,

        /// <summary>
        /// 唯一索引
        /// </summary>
        Unique,

        /// <summary>
        /// 主键
        /// </summary>
        PrimaryKey,

        /// <summary>
        /// 外键
        /// </summary>
        ForeignKey,

        /// <summary>
        /// 全文索引
        /// </summary>
        FullText

    }
}
