using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yunyong.DataExchange.Common;

namespace Yunyong.DataExchange.UserFacade.Update
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
