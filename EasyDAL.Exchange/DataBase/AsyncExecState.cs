using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.DataBase
{
    internal struct AsyncExecState
    {
        public readonly DbCommand Command;
        public readonly Task<int> Task;
        public AsyncExecState(DbCommand command, Task<int> task)
        {
            Command = command;
            Task = task;
        }
    }
}
