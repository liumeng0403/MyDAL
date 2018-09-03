using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Update
{
    public class UpdateFilter<M>:Operator
    {        
        internal UpdateFilter(DbContext dc)
        {
            DC = dc;
        }

        /// <summary>
        /// 与条件
        /// </summary>
        /// <param name="func">格式: it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109")</param>
        public UpdateFilter<M> And(Expression<Func<M, bool>> func)
        {
            AndHandle(func,CrudTypeEnum.Update);
            return this;
        }

        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074")</param>
        public UpdateFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func, CrudTypeEnum.Update);
            return this;
        }
        
        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        public async Task<int> UpdateAsync()
        {
            return await SqlHelper.ExecuteAsync(
                DC.Conn, 
                DC.SqlProvider.GetSQL<M>( SqlTypeEnum.UpdateAsync)[0],
                DC.GetParameters());
        }

    }
}
