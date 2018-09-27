using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Update
{
    public class WhereU<M> 
        : Operator, IUpdate
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
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.UpdateAsync)[0],
                DC.GetParameters());
        }

    }
}
