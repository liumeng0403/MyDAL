using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using EasyDAL.Exchange.Interfaces;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Impls
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
