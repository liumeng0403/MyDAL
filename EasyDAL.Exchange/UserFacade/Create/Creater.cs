using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Create
{
    public class Creater<M> 
        : Operator, ICreate<M>, ICreateBatch<M>
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
            return await new CreateImpl<M>(DC).CreateAsync(m);
        }
        
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateBatchAsync(IEnumerable<M> mList)
        {
            return await new CreateBatchImpl<M>(DC).CreateBatchAsync(mList);
        }

    }
}
