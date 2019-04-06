using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class DeleteImpl<M>
        : Impler
        , IDelete, IDeleteSync
        where M:class
    {
        internal DeleteImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> DeleteAsync()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return await DC.DS.ExecuteNonQueryAsync();
        }

        public int Delete()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return DC.DS.ExecuteNonQuery();
        }

    }
}
