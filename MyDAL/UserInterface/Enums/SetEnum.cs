namespace MyDAL
{
    public enum SetEnum
    {
        /// <summary>
        /// 允许赋 null 值,默认
        /// </summary>
        AllowedNull,
        /// <summary>
        /// 不允许赋 null 值,抛异常
        /// </summary>
        NotAllowedNull,
        /// <summary>
        /// 赋 null 值的字段自动忽略,只 set 赋值不为 null 的字段,若全部为 null 则抛异常
        /// </summary>
        IgnoreNull
    }
}
