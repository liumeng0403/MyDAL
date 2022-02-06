using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region Insert API

        /// <summary>
        /// Inserter 便捷 InsertAsync 方法
        /// </summary>
        public static async Task<int> InsertAsync<M>(this XConnection conn, M m)
            where M : class, new()
        {
            return await conn.Inserter<M>().InsertAsync(m);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Inserter 便捷 InsertAsync 方法 , SQL 语句
        /// </summary>
        public static async Task<int> InsertAsync(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckCreate(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Inserter 便捷 Insert 方法
        /// </summary>
        public static int Insert<M>(this XConnection conn, M m)
            where M : class, new()
        {
            return conn.Inserter<M>().Insert(m);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Inserter 便捷 Insert 方法 , SQL 语句
        /// </summary>
        public static int Insert(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckCreate(sql);
            return conn.ExecuteNonQuery(sql, dbParas);
        }
        
        #endregion

    }
}
