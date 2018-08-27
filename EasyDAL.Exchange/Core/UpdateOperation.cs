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
            //Fields = new List<string>();
            //Changes = new List<string>();
        }

        //private List<string> Fields { get; set; }
        //private List<string > Changes { get; set; }

        public UpdateOperation<M> Set<F>(Expression<Func<M,F>> func,F newVal)
        {
            var key = EH.ExpressionHandle(func);  
            //Fields.Add(key);
            DC.Conditions.Add(new DicModel<string, string>
            {
                key= key,
                Value=GH.GetTypeValue(newVal.GetType(),newVal),
                Option= OptionEnum.Set,
                Action= ActionEnum.Set
            });
            return this;
        }

        public UpdateOperation<M> Change<F>(Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            var key = EH.ExpressionHandle(func);
            //Fields.Add(key);
            DC.Conditions.Add(new DicModel<string, string>
            {
                key = key,
                Value = GH.GetTypeValue(modifyVal.GetType(), modifyVal),
                Option = GetChangeOption(change),
                Action = ActionEnum.Change
            });
            return this;
        }

        public UpdateOperation<M> Where(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            DC.Conditions.Add(field);
            return this;
        }

        public UpdateOperation<M> And(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            DC.Conditions.Add(field);
            return this;
        }

        public UpdateOperation<M> Or(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            DC.Conditions.Add(field);
            return this;
        }

        public async Task<int> UpdateAsync()
        {
            TryGetTableName<M>(out var tableName);

            var updateFields = GetUpdates();
            var wherePart = GetWheres();
            var paras = GetParameters();
            var sql = $" update `{tableName}` set {updateFields} where {wherePart} ;";

            return await SqlMapper.ExecuteAsync(DC.Conn, sql, paras);
        }

    }
}
