using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region Delete API

        /*-------------------------------------------------------------*/

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static int Delete<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Deleter<M>().Where(compareFunc).Delete();
        }

        /*-------------------------------------------------------------*/

        public static int Delete(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckDelete(sql);
            return conn.ExecuteNonQuery(sql, dbParas);
        }
        
        #endregion

    }
}
