using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class QuerySingleValueImpl<M>
        : Impler, IQuerySingleValue<M>
        where M: class
    {
        internal QuerySingleValueImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await DC.DS.ExecuteScalarAsync<V>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QuerySingleValueAsync)[0],
                DC.DPH.GetParameters(DC.Parameters));
        }
    }
}
