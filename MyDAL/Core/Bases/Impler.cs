using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core.Bases
{
    internal abstract class Impler
        : Operator
    {
        /**********************************************************************************************************/

        private void SetInsertValue<M>(M m, int index)
        {
            var key = DC.SC.GetModelKey(m.GetType().FullName);
            var props = DC.SC.GetModelProperys(key);
            var columns = DC.SC.GetColumnInfos(key);
            var fullName = typeof(M).FullName;

            var list = new List<DicParam>();
            foreach (var prop in props)
            {
                var val = DC.VH.PropertyValue(prop, m);
                DC.Compare = CompareEnum.None;
                list.Add(DC.DPH.InsertHelperDic(fullName, prop.Name, val, prop.PropertyType));
            }
            DC.DPH.AddParameter(DC.DPH.InsertDic(fullName, list));
        }

        /**********************************************************************************************************/

        protected async Task<PagingList<T>> PagingListAsyncHandle<T>(UiMethodEnum sqlType, bool single)
        {
            var result = new PagingList<T>();
            result.PageIndex = DC.PageIndex.Value;
            result.PageSize = DC.PageSize.Value;
            PreExecuteHandle(sqlType);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>();
            if (single)
            {
                result.Data = await DC.DS.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                result.Data = await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
            return result;
        }
        protected async Task<PagingList<T>> PagingListAsyncHandle<M, T>(UiMethodEnum sqlType, bool single, Func<M, T> mapFunc)
            where M : class
        {
            var result = new PagingList<T>();
            result.PageIndex = DC.PageIndex.Value;
            result.PageSize = DC.PageSize.Value;
            PreExecuteHandle(sqlType);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>();
            if (single)
            {
                result.Data = await DC.DS.ExecuteReaderSingleColumnAsync(mapFunc);
            }
            else
            {
                result.Data = await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
            return result;
        }

        /**********************************************************************************************************/

        protected void SingleColumnHandle<M, T>(Expression<Func<M, T>> propertyFunc)
            where M : class
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.DPH.AddParameter(DC.EH.FuncMFExpression(propertyFunc)[0]);
        }
        protected void SingleColumnHandle<T>(Expression<Func<T>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.DPH.AddParameter(DC.EH.FuncTExpression(propertyFunc)[0]);
        }

        /**********************************************************************************************************/

        protected void SelectMHandle<M>()
        {
            DC.Action = ActionEnum.Select;
            var mType = typeof(M);
            var fullName = mType.FullName;
            var dic = DC.Parameters.FirstOrDefault(it => fullName.Equals(it.ClassFullName, StringComparison.OrdinalIgnoreCase));
            if (dic != null)
            {
                DC.Option = OptionEnum.Column;
                DC.Compare = CompareEnum.None;
                DC.DPH.AddParameter(DC.DPH.ColumnDic("*", dic.TableAliasOne, fullName, dic.PropOne));
            }
            else if (DC.Parameters.Count == 0)
            {
                // important
            }
            else
            {
                var fullNames = DC.Parameters.Where(it => !string.IsNullOrWhiteSpace(it.ClassFullName)).Distinct();
                throw new Exception($"请使用 [[Task<List<VM>> ListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {mType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.ClassName))}]] 其中之一 !");
            }
        }
        protected void SelectMHandle<M, VM>()
        {
            var mType = typeof(M);
            var vmType = typeof(VM);
            if (mType == vmType)
            {
                return;
            }

            //
            DC.Action = ActionEnum.Select;
            var fullName = mType.FullName;
            var mProps = DC.GH.GetPropertyInfos(mType);
            var tab = DC.Parameters.FirstOrDefault(it => fullName.Equals(it.ClassFullName, StringComparison.OrdinalIgnoreCase));
            var vmProps = DC.GH.GetPropertyInfos(vmType);
            if (tab != null)
            {
                DC.Option = OptionEnum.Column;
                DC.Compare = CompareEnum.None;
                foreach (var prop in mProps)
                {
                    foreach (var vProp in vmProps)
                    {
                        if (prop.Name.Equals(vProp.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            DC.DPH.AddParameter(DC.DPH.ColumnDic(prop.Name, tab.TableAliasOne, fullName, prop.Name));
                        }
                    }
                }
            }
            else if (DC.Parameters.Count == 0)
            {
                // important
            }
            else
            {
                var fullNames = DC.Parameters.Where(it => !string.IsNullOrWhiteSpace(it.ClassFullName)).Distinct();
                throw new Exception($"请使用 [[Task<List<VM>> ListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {mType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.ClassName))}]] 其中之一 !");
            }
        }
        protected void SelectMHandle<VM>(Expression<Func<VM>> func)
        {
            DC.Action = ActionEnum.Select;
            var list = DC.EH.FuncTExpression(func);
            foreach (var dic in list)
            {
                dic.Option = OptionEnum.ColumnAs;
                DC.DPH.AddParameter(dic);
            }
        }
        protected void SelectMHandle<M, VM>(Expression<Func<M, VM>> propertyFunc)
            where M : class
        {
            DC.Action = ActionEnum.Select;
            var list = DC.EH.FuncMFExpression(propertyFunc);
            foreach (var dic in list)
            {
                dic.Option = OptionEnum.ColumnAs;
                DC.DPH.AddParameter(dic);
            }
        }

        /**********************************************************************************************************/

        protected void CreateMHandle<M>(M m)
        {
            DC.Option = OptionEnum.Insert;
            SetInsertValue(m, 0);
        }
        protected void CreateMHandle<M>(IEnumerable<M> mList)
        {
            DC.Option = OptionEnum.InsertTVP;
            var i = 0;
            foreach (var m in mList)
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
