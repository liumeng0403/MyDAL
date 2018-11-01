using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQuerySingleValue<M>
        where M:class
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
