using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using EasyDAL.Exchange.Interfaces;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Impls
{
    internal class ExistImpl<M>
        : Impler, IExist
    {
        internal ExistImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<bool> ExistAsync()
        {
            var count = await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.ExistAsync)[0],
                DC.GetParameters());
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
