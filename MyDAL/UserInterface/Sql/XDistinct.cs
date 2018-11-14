using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Query;

namespace Yunyong.DataExchange
{
    public static class XDistinct
    {
        public static Selecter<M> Distinct<M>(this Selecter<M> selecter)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Select;
            selecter.DC.Option = OptionEnum.Distinct;
            selecter.DC.Compare = CompareEnum.None;
            selecter.DC.Func = FuncEnum.None;
            selecter.DC.DPH.AddParameter(selecter.DC.DPH.DistinctDic());
            return selecter;
        }
    }
}
