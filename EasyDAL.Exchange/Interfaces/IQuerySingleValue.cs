using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQuerySingleValue
    {
        Task<V> QuerySingleValueAsync<V>();
    }
}
