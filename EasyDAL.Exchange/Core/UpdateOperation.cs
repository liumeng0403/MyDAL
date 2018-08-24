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

        public UpdateOperation<M> Set(Expression<Func<M,bool>> func)
        {
            var field = EH.ExpressionHandle(func);  
            Fields.Add(field.key);
            field.Action = ActionEnum.Set;
            Conditions.Add(field);
            return this;
        }

        public UpdateOperation<M> Where(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            Conditions.Add(field);
            return this;
        }

        public UpdateOperation<M> And(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            Conditions.Add(field);
            return this;
        }

        public UpdateOperation<M> Or(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            Conditions.Add(field);
            return this;
        }

        public async Task<int> UpdateAsync()
        {
            TryGetTableName<M>(out var tableName);

            if (!Fields.Any())
            {
                throw new Exception("没有设置任何要更新的字段!");
            }
            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何更新条件!");
            }
            var updateFields = string.Join(",", Fields.Select(p => $"`{p}`=@{p}"));
            var wherePart = GetWheres();
            var paras = GetParameters();
            var sql = $" update `{tableName}` set {updateFields} where {wherePart} ;";

            return await SqlMapper.ExecuteAsync(Conn, sql, paras);
        }

    }
}
