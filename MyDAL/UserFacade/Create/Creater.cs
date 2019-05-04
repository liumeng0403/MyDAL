using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Create
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Creater<M>
        : Operator
        , ICreateAsync<M>, ICreate<M>
        , ICreateBatchAsync<M>, ICreateBatch<M>
        where M : class
    {
        internal Creater(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateAsync(M m)
        {
            return await new CreateAsyncImpl<M>(DC).CreateAsync(m);
        }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public int Create(M m)
        {
            return new CreateImpl<M>(DC).Create(m);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateBatchAsync(IEnumerable<M> mList)
        {
            return await new CreateBatchAsyncImpl<M>(DC).CreateBatchAsync(mList);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public int CreateBatch(IEnumerable<M> mList)
        {
            return new CreateBatchImpl<M>(DC).CreateBatch(mList);
        }

    }
}
