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

namespace EasyDAL.Exchange.UserFacade.Update
{
    public class UpdateFilter<M>:Operator,IMethodObject
    {        
        internal UpdateFilter(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
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
