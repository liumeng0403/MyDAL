using MyDAL.Core;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Update
{
    public class WhereU<M> 
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
