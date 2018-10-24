using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryAll<M>
        where M : class
    {
        Task<List<M>> QueryAllAsync();
        Task<List<VM>> QueryAllAsync<VM>()
            where VM : class;


    }
}
