using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IQuerySingleValue
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
