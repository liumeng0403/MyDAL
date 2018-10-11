using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.ExpressionX;
using MyDAL.Core.Helper;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query,typeof(M).FullName, "*"));
            DC.IP.ConvertDic();
            var count = await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.ExistAsync)[0],
                DC.SqlProvider.GetParameters());
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
