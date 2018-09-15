using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Update
{
    public class UpdateFilter<M> : Operator, IMethodObject
    {
        internal UpdateFilter(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        public async Task<int> UpdateAsync()
        {
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.UpdateAsync)[0],
                DC.GetParameters());
        }

    }
}
