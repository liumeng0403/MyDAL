using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Query
{
    public class CountQ<M> : Operator, IMethodObject
    {

        internal CountQ(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表单值查询
        /// </summary>
        /// <typeparam name="V">int/long/decimal/...</typeparam>
        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await SqlHelper.ExecuteScalarAsync<V>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QuerySingleValueAsync)[0],
                DC.GetParameters());
        }

    }
}
