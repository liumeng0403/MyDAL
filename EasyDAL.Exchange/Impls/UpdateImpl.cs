using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class UpdateImpl<M>
        : Impler, IUpdate<M>
    {
        internal UpdateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> UpdateAsync()
        {
            return await DC.DS.ExecuteNonQueryAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.UpdateAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
