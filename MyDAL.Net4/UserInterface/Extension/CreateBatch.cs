using System.Collections.Generic;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region CreateBatch API

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static int CreateBatch<M>(this XConnection conn, IEnumerable<M> mList)
            where M : class, new()
        {
            return conn.Creater<M>().CreateBatch(mList);
        }

        #endregion

    }
}
