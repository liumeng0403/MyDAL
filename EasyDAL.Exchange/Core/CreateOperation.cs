using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Base;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public class CreateOperation<M> : DbOperation
    {
        public CreateOperation(IDbConnection conn)
            : base(conn)
        {
        }


        public async Task<int> CreateAsync(M m)
        {
            TryGetTableName(m, out var tableName);

            var properties = GetProperties(m);
            var columns = string.Join(",", properties.Select(p => "`" + p + "`"));
            var paras = string.Join(",", properties.Select(p => "@" + p));
            var sql = $" insert into `{tableName}` ({columns}) values ({paras}) ;";

            return await SqlMapper.ExecuteAsync(DC.Conn,sql, m);

        }

    }
}
