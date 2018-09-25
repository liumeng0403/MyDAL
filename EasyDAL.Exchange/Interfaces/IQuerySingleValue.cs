using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IQuerySingleValue
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
