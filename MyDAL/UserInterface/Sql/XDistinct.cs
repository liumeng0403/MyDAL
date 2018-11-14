using MyDAL.Core.Enums;
using MyDAL.UserFacade.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL
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
