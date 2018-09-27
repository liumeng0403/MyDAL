using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class DeleteImpl<M>
        : Impler, IDelete
    {
        internal DeleteImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> DeleteAsync()
        {
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.DeleteAsync)[0],
                DC.GetParameters());
        }
    }
}
