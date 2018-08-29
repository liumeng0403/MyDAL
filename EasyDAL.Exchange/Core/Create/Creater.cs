using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Create
{
    public class Creater<M>: Operator
    {
        internal Creater(DbContext dc)
        {
            DC = dc;
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.GetProperties(m);
            return await SqlMapper.ExecuteAsync(
                DC.Conn, 
                DC.SqlProvider.GetSQL<M>( SqlTypeEnum.CreateAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
