using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    public class CountQ<M> 
        : Operator, IQuerySingleValue
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
