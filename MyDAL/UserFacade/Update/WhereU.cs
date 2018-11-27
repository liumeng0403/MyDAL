using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Update
{
    public sealed class WhereU<M>
        : Operator, IUpdate<M>
        where M:class
    {
        internal WhereU(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        public async Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull)
        {
            return await new UpdateImpl<M>(DC).UpdateAsync(set);
        }

    }
}
