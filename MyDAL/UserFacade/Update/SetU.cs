using System;
using System.Threading.Tasks;
using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using MyDAL.Interfaces.Segments;

namespace MyDAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class SetU<M>
        : Operator
        , IWhereU<M>
        , IUpdateAsync<M>, IUpdate<M>
        where M : class
    {

        internal SetU(Context dc)
            : base(dc)
        { }

        public WhereU<M> WhereSegment
        {
            get
            {
                return new WhereU<M>(DC);
            }
        }

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会更新表中所有数据！！！", false)]
        public async Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull)
        {
            return await new UpdateAsyncImpl<M>(DC).UpdateAsync(set);
        }

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会更新表中所有数据！！！", false)]
        public int Update(SetEnum set = SetEnum.AllowedNull)
        {
            return new UpdateImpl<M>(DC).Update(set);
        }
    }
}
