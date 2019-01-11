﻿using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Core.Bases
{
    internal abstract class Impler
        : Operator
    {
        /**********************************************************************************************************/

        private void SetInsertValue<M>(M m, int index)
        {
            var tbm = DC.XC.GetTableModel(m.GetType());
            var list = new List<DicParam>();
            foreach (var prop in tbm.TbMProps)
            {
                var val = DC.VH.PropertyValue(prop, m);
                DC.Compare = CompareXEnum.None;
                list.Add(DC.DPH.InsertHelperDic(tbm.TbMFullName, prop.Name, val, prop.PropertyType));
            }
            DC.DPH.AddParameter(DC.DPH.InsertDic(tbm.TbMFullName, list));
        }

        /**********************************************************************************************************/

        protected async Task<PagingResult<T>> PagingListAsyncHandle<T>(UiMethodEnum sqlType, bool single)
        {
            PreExecuteHandle(sqlType);
            return await DC.DS.ExecuteReaderPagingAsync<None, T>(single, null);
        }
        protected async Task<PagingResult<T>> PagingListAsyncHandle<M, T>(UiMethodEnum sqlType, bool single, Func<M, T> mapFunc)
            where M : class
        {
            PreExecuteHandle(sqlType);
            return await DC.DS.ExecuteReaderPagingAsync(single, mapFunc);
        }

        /**********************************************************************************************************/

        protected void SingleColumnHandle<M, T>(Expression<Func<M, T>> propertyFunc)
            where M : class
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            var col = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(col);
        }
        protected void SingleColumnHandle<T>(Expression<Func<T>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            var col = DC.XE.FuncTExpression(propertyFunc);
            DC.DPH.AddParameter(col);
        }

        /**********************************************************************************************************/

        protected void SelectMHandle<M>()
        {
            DC.Action = ActionEnum.Select;
            var mType = typeof(M);
            var fullName = mType.FullName;
            var dic = DC.Parameters.FirstOrDefault(it => fullName.Equals(it.TbMFullName, StringComparison.OrdinalIgnoreCase));
            if (dic != null)
            {
                DC.Option = OptionEnum.Column;
                DC.Compare = CompareXEnum.None;
                var col = DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.ColumnDic("*", dic.TbAlias, fullName, dic.TbMProp) });
                DC.DPH.AddParameter(col);
            }
            else if (DC.Parameters.Count == 0)
            {
                // important
            }
            else
            {
                var fullNames = DC.Parameters.Where(it => !string.IsNullOrWhiteSpace(it.TbMFullName)).Distinct();
                throw new Exception($"请使用 [[Task<List<VM>> ListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {mType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.TbMName))}]] 其中之一 !");
            }
        }
        protected void SelectMQ<M, VM>()
        {
            var tbm = DC.XC.GetTableModel(typeof(M));
            var vmType = typeof(VM);
            if (tbm.TbMType == vmType)
            {
                return;
            }

            //
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            var vmProps = DC.GH.GetPropertyInfos(vmType);
            var list = new List<DicParam>();
            foreach (var prop in tbm.TbMProps)
            {
                foreach (var vProp in vmProps)
                {
                    if (prop.Name.Equals(vProp.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(DC.DPH.ColumnDic(prop.Name, string.Empty, tbm.TbMFullName, prop.Name));
                    }
                }
            }
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(list));
        }
        protected void SelectMHandle<VM>(Expression<Func<VM>> func)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.ColumnAs;
            var col = DC.XE.FuncTExpression(func);
            DC.DPH.AddParameter(col);
        }
        protected void SelectMHandle<M, VM>(Expression<Func<M, VM>> propertyFunc)
            where M : class
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.ColumnAs;
            var col = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(col);
        }

        /**********************************************************************************************************/

        protected void CreateMHandle<M>(IEnumerable<M> ms)
        {
            DC.Option = OptionEnum.Insert;
            var i = 0;
            foreach (var m in ms)
            {
                SetInsertValue(m, i);
                i++;
            }
        }

        /**********************************************************************************************************/

        protected void PreExecuteHandle(UiMethodEnum method)
        {
            DC.DPH.SetParameter();
            DC.Method = method;
            DC.SqlProvider.GetSQL();
        }

        /**********************************************************************************************************/

        protected Impler(Context dc)
            : base(dc)
        {
        }
    }
}
