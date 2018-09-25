using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.UserFacade.Interfaces
{
    internal interface IDelete
    {
        Task<int> DeleteAsync();
    }
}
