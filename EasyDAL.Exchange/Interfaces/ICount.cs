using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface ICount<M>
    {
        Task<long> CountAsync();
        Task<long> CountAsync<F>(Expression<Func<M, F>> func);
    }
}
