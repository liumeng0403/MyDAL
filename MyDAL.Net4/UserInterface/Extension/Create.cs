using System.Collections.Generic;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        // create 要支持自增 id 

        #region Create API

        /*-------------------------------------------------------------*/

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static int Create<M>(this XConnection conn, M m)
            where M : class, new()
        {
            return conn.Creater<M>().Create(m);
        }

        /*-------------------------------------------------------------*/

        public static int Create(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckCreate(sql);
            return conn.ExecuteNonQuery(sql, dbParas);
        }
        
        #endregion

    }
}
