using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class CreateImpl<M>
        : Impler, ICreate<M>
    {
        public CreateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.GetProperties(m);
            DC.IP.ConvertDic();
            return await DC.DS.ExecuteNonQueryAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
