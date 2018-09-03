using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Delete
{
    public class DeleteFilter<M>:Operator
    {        
        internal DeleteFilter(DbContext dc)
        {
            DC = dc;
        }

        /// <summary>
        /// 与 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public DeleteFilter<M> And(Expression<Func<M, bool>> func)
        {
            AndHandle(func,CrudTypeEnum.Delete);
            return this;
        }

        /// <summary>
        /// 或 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public DeleteFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func, CrudTypeEnum.Delete);
            return this;
        }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        /// <returns>删除条目数</returns>
        public async Task<int> DeleteAsync()
        {
            return await SqlHelper.ExecuteAsync(
                DC.Conn, 
                DC.SqlProvider.GetSQL<M>( SqlTypeEnum.DeleteAsync)[0],
                DC.GetParameters());
        }

    }
}
