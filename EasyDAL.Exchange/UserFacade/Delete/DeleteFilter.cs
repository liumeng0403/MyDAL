using System.Threading.Tasks;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Helper;

namespace Yunyong.DataExchange.UserFacade.Delete
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
