using System;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IBusinessUnitOption
    {
        Task<string> BusinessUnitOption(Func<Task<string>> businessFunc);
        Task<(string errMsg, M data)> BusinessUnitOption<M>(Func<Task<M>> businessFunc);
        Task<(string errMsg, M data)> BusinessUnitOption<M>(Func<Task<(string errMsg, M data)>> businessFunc);
    }
}
