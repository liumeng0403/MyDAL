using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region InsertBatch API
        
        /// <summary>
        /// Inserter 便捷 InsertBatch 方法
        /// </summary>
        public static int InsertBatch<M>(this XConnection conn, IEnumerable<M> mList)
            where M : class, new()
        {
            return conn.Inserter<M>().InsertBatch(mList);
        }

        #endregion

    }
}
