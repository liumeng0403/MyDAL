using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class CreateImpl<M>
        : Impler, ICreate<M>
        where M:class
    {
        public CreateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(m);
            PreExecuteHandle(UiMethodEnum.CreateAsync);
            return await DC.DS.ExecuteNonQueryAsync();
        }
    }
}
