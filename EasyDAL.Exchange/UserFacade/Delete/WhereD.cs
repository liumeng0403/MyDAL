using MyDAL.Core;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Delete
{
    public class WhereD<M> 
        : Operator, IDelete
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
