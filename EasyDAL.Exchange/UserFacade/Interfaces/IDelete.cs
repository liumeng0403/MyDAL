using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Interfaces
{
    internal interface IDelete
    {
        Task<int> DeleteAsync();
    }
}
