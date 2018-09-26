using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface ICount<M>
    {
        Task<long> CountAsync();
        Task<long> CountAsync<F>(Expression<Func<M, F>> func);
    }
}
