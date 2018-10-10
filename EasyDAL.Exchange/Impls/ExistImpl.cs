using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.ExpressionX;
using MyDAL.Helper;
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query, "*"));
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
