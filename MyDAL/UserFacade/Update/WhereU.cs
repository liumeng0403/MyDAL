using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereU<M>
        : Operator
        , IUpdateAsync<M>, IUpdate<M>
        where M : class
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
            return await new UpdateAsyncImpl<M>(DC).UpdateAsync(set);
        }

        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        public int Update(SetEnum set = SetEnum.AllowedNull)
        {
            return new UpdateImpl<M>(DC).Update(set);
        }
    }
}
