using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Update
{
    public sealed class WhereU<M>
        : Operator, IUpdate<M>
    {
        internal WhereU(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        public async Task<int> UpdateAsync()
        {
            return await new UpdateImpl<M>(DC).UpdateAsync();
        }

    }
}
