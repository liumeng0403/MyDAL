using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Delete
{
    public sealed class WhereD<M> 
        : Operator, IDelete
        where M:class
    {
        internal WhereD(Context dc)
            : base(dc)
        { }
        
        /// <summary>
        /// 单表数据删除
        /// </summary>
        /// <returns>删除条目数</returns>
        public async Task<int> DeleteAsync()
        {
            return await new DeleteImpl<M>(DC).DeleteAsync();
        }

    }
}
