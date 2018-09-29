using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IQuerySingleValue<M>
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
