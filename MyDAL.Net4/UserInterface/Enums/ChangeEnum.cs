namespace MyDAL
{
    /// <summary>
    /// 用于 update 语句 set 字段，以生成 SQL 如： table.column1=table.column1+num  ... ...
    /// </summary>
    public enum ChangeEnum
    {
        /// <summary>
        /// +
        /// </summary>        
        Add,
        /// <summary>
        /// -
        /// </summary>        
        Minus
    }
}
