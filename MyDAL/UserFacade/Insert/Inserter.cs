using System.Collections.Generic;
using System.Threading.Tasks;
using MyDAL.Core.Bases;
using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;

namespace MyDAL.UserFacade.Insert
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Inserter<M>
        : Operator
        , IInsertAsync<M>, IInsert<M>
        , IInsertBatchAsync<M>, IInsertBatch<M>
        where M : class
    {
        internal Inserter(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> InsertAsync(M m)
        {
            return await new InsertAsyncImpl<M>(DC).InsertAsync(m);
        }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public int Insert(M m)
        {
            return new InsertImpl<M>(DC).Insert(m);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> InsertBatchAsync(IEnumerable<M> mList)
        {
            return await new InsertBatchAsyncImpl<M>(DC).InsertBatchAsync(mList);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public int InsertBatch(IEnumerable<M> mList)
        {
            return new InsertBatchImpl<M>(DC).InsertBatch(mList);
        }

    }
}
