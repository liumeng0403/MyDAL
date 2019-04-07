using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class DeleteImpl<M>
        : Impler
        , IDeleteAsync, IDelete
        where M:class
    {
        internal DeleteImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> DeleteAsync()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return await DC.DSA.ExecuteNonQueryAsync();
        }

        public int Delete()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return DC.DSS.ExecuteNonQuery();
        }

    }
}
