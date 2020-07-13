using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region Update API

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<int> UpdateAsync<M>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject)
            where M : class, new()
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync();
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> UpdateAsync(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckUpdate(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas);
        }

        /*-------------------------------------------------------------*/


        /*-------------------------------------------------------------*/


        #endregion

    }
}
