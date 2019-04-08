using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class DeleteAsyncImpl<M>
    : ImplerAsync
    , IDeleteAsync 
    where M : class
    {
        internal DeleteAsyncImpl(Context dc)
            : base(dc)
        {       }

        public async Task<int> DeleteAsync()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return await DSA.ExecuteNonQueryAsync();
        }
         
    }
    internal sealed class DeleteImpl<M>
        : ImplerSync
        , IDelete
        where M:class
    {
        internal DeleteImpl(Context dc) 
            : base(dc)
        {
        }
         
        public int Delete()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return DSS.ExecuteNonQuery();
        }

    }
}
