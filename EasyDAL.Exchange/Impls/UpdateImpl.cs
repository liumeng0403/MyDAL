using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
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
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.UpdateAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
