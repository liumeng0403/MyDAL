using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQuerySingleValue<M>
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
