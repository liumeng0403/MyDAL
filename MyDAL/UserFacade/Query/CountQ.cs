using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    public sealed  class CountQ<M> 
        : Operator, IQuerySingleValue<M>
        where M:class
    {

        internal CountQ(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表单值查询
        /// </summary>
        /// <typeparam name="V">int/long/decimal/...</typeparam>
        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await new QuerySingleValueImpl<M>(DC).QuerySingleValueAsync<V>();
        }

    }
}
