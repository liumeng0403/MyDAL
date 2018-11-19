using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class UpdateImpl<M>
        : Impler, IUpdate<M>
        where M:class
    {
        internal UpdateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> UpdateAsync()
        {
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return await DC.DS.ExecuteNonQueryAsync();
        }
    }
}
