using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EasyDAL.Exchange.Common;

namespace EasyDAL.Exchange.UserFacade.Delete
{
    public class DeleteFilter<M>:Operator,IMethodObject
    {        
        internal DeleteFilter(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
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
