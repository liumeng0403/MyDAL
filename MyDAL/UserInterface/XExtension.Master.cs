using MyDAL.Impls.ImplAsyncs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        /*
         *  剔除 sql server 支持, 拥抱开源 , 仅支持 mysql 
         *  [EditorBrowsable(EditorBrowsableState.Never)]
         *  [Obsolete("Use Register(Component.For<T>().Lifestyle.Is(lifestyle)) instead.")] 
         */

        #region Internal

        /* 内部方法 */

        private static async Task<int> ExecuteNonQueryAsync(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            var dc = DcForSQL(conn, sql, dbParas);
            return await new ExecuteNonQuerySQLAsyncImpl(dc).ExecuteNonQueryAsync();
        }

        #endregion



    }
}
