using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IQuerySingleValue<M>
        where M:class
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
