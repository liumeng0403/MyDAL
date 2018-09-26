using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using EasyDAL.Exchange.Impls;
using EasyDAL.Exchange.Interfaces;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Delete
{
    public class WhereD<M> 
        : Operator, IDelete
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
            return await new DeleteImpl<M>(DC).DeleteAsync();
        }

    }
}
