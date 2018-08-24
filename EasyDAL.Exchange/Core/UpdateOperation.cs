using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Base;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public class UpdateOperation<M> : DbOperation
    {
        public UpdateOperation(IDbConnection conn) 
            : base(conn)
        {
            Fields = new List<string>();
            Changes = new List<string>();
        }

        private List<string> Fields { get; set; }
        private List<string > Changes { get; set; }

        public UpdateOperation<M> Set<T>(Expression<Func<M,T>> func)
        {
            var field = EH.ExpressionHandle(func);
            Fields.Add(field);
            return this;
        }

        public UpdateOperation<M> Where<T>(Expression<Func<M,T>> func)
        {
            var field = EH.ExpressionHandle(func);
            Conditions.Add(new DicModel<string, string>
            {
                key = field,
                Value = null,
                Option =  OptionEnum.None
            });
            return this;
        }

        public async Task<int> UpdateAsync(M m)
        {
            TryGetTableName(m, out var tableName);

            if (!Fields.Any())
            {
                throw new Exception("没有设置任何要更新的字段!");
            }
            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何更新条件!");
            }

            var updateFields = string.Join(",", Fields.Select(p => $"`{p}`=@{p}"));
            var whereFields = " where " + string.Join(" and ", Conditions.Select(p => $"`{p.key}`=@{p.key}"));


            var sql = $" update `{tableName}` set {updateFields}{whereFields} ;";
            return await SqlMapper.ExecuteAsync(Conn, sql, m);
        }

    }
}
