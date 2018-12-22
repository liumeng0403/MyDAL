using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class CountImpl<M>
        : Impler, ICount<M>
        where M : class
    {
        internal CountImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CountAsync()
        {
            DC.Action = ActionEnum.Select;
            //DC.Option = OptionEnum.Count;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M).FullName, "*") }));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }

        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.EH.FuncMFExpression(propertyFunc);
            //var key = keyDic.ColumnOne;
            //DC.Option = OptionEnum.Count;
            //DC.DPH.AddParameter(DC.DPH.CountDic(typeof(M).FullName, key));
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }
    }

    internal class CountXImpl
        : Impler, ICountX
    {
        public CountXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CountAsync()
        {
            DC.Action = ActionEnum.Select;
            //DC.Option = OptionEnum.Count;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(string.Empty, "*", string.Empty) }));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }

        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.EH.FuncTExpression(propertyFunc);
            //DC.Option = OptionEnum.Count;
            //DC.DPH.AddParameter(DC.DPH.CountDic(dic.ClassFullName, dic.ColumnOne, dic.TableAliasOne));
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }
    }
}
