using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using EasyDAL.Exchange.UserFacade.Interfaces;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Delete
{
    public class WhereD<M> 
        : Operator, IMethodObject, IDelete
    {
        internal WhereD(Context dc)
            : base(dc)
        { }
        
        /// <summary>
        /// 单表数据删除
        /// </summary>
        /// <returns>删除条目数</returns>
        public async Task<int> DeleteAsync()
        {
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.DeleteAsync)[0],
                DC.GetParameters());
        }

    }
}
