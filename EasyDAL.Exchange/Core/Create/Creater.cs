using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
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
            DC.OP.TryGetTableName(m, out var tableName);

            var properties = DC.OP.GetProperties(m);
            var columns = string.Join(",", properties.Select(p => "`" + p + "`"));
            var paras = string.Join(",", properties.Select(p => "@" + p));
            var sql = $" insert into `{tableName}` ({columns}) values ({paras}) ;";

            return await SqlMapper.ExecuteAsync(DC.Conn, sql, m);

        }
    }
}
