using System;
using System.Linq.Expressions;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region IsExist API

        /// <summary>
        /// Selecter 便捷-同步 IsExist 方法
        /// </summary>
        public static bool IsExist<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).IsExist();
        }
        
        #endregion

    }
}
