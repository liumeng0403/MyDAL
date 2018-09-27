using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
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
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateAsync)[0],
                DC.GetParameters());
        }
    }
}
