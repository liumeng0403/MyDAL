using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
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
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(typeof(M).FullName, "*"));
            DC.IP.ConvertDic();
            var count = await DC.DS.ExecuteScalarAsync<long>(
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
