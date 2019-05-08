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
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync(set);
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> UpdateAsync(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckUpdate(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static int Update<M>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).Update(set);
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
