using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class DeleteImpl<M>
        : Impler, IDelete
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
    }
}
