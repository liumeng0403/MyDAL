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

        #region Update API


        /*-------------------------------------------------------------*/


        /*-------------------------------------------------------------*/

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static int Update<M>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject)
            where M : class, new()
        {
            return conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).Update();
        }

        /*-------------------------------------------------------------*/

        public static int Update(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckUpdate(sql);
            return conn.ExecuteNonQuery(sql, dbParas);
        }
        
        #endregion

    }
}
