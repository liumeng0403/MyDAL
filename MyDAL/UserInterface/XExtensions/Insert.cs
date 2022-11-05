using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {
        
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

    }
}
