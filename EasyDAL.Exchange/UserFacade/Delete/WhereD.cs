using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Delete
{
    public sealed class WhereD<M> 
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
