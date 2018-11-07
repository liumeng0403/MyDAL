using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
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

            foreach (var prop in props)
            {
                var val = DC.VH.PropertyValue(prop, m);
                DC.Compare = CompareEnum.None;
                DC.DPH.AddParameter(DC.DPH.InsertDic(fullName, prop.Name, val, prop.PropertyType, index));
            }
        }

        /**********************************************************************************************************/

        protected async Task<PagingList<M>> QueryPagingListAsyncHandle<M>(int pageIndex, int pageSize, UiMethodEnum sqlType)
            where M : class
        {
            var result = new PagingList<M>();
            DC.PageIndex = result.PageIndex = pageIndex;
            DC.PageSize= result.PageSize = pageSize;
            DC.Method = sqlType;            
            DC.SqlProvider.GetSQL<M>();
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>();
            result.Data = await DC.DS.ExecuteReaderMultiRowAsync<M>();
            return result;
        }

        protected async Task<PagingList<VM>> QueryPagingListAsyncHandle<M, VM>(int pageIndex, int pageSize, UiMethodEnum sqlType)
            where VM : class
        {
            var result = new PagingList<VM>();
            DC.PageIndex = result.PageIndex = pageIndex;
            DC.PageSize = result.PageSize = pageSize;
            DC.Method = sqlType;
            DC.SqlProvider.GetSQL<M>();
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>();
            result.Data = await DC.DS.ExecuteReaderMultiRowAsync<VM>();
            return result;
        }

        /**********************************************************************************************************/

        internal void SelectMHandle<M>()
        {
            DC.Action = ActionEnum.Select;
            var mType = typeof(M);
            var fullName = mType.FullName;
            var tab = DC.Parameters.FirstOrDefault(it => fullName.Equals(it.ClassFullName, StringComparison.OrdinalIgnoreCase));
            if (tab != null)
            {
                DC.Option = OptionEnum.Column;
                DC.Compare = CompareEnum.None;
                DC.DPH.AddParameter(DC.DPH.ColumnDic("*", tab.TableAliasOne, fullName));
            }
            else if (DC.Parameters.Count == 0)
            {
                // important
            }
            else
            {
                var fullNames = DC.Parameters.Where(it => !string.IsNullOrWhiteSpace(it.ClassFullName)).Distinct();
                throw new Exception($"请使用 [[Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {mType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.ClassName))}]] 其中之一 !");
            }
        }

        internal void SelectMHandle<M, VM>()
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
                            DC.DPH.AddParameter(DC.DPH.ColumnDic(prop.Name, tab.TableAliasOne, fullName));
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
                throw new Exception($"请使用 [[Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {mType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.ClassName))}]] 其中之一 !");
            }
        }

        internal void SelectMHandle<VM>(Expression<Func<VM>> func)
        {
            DC.Action = ActionEnum.Select;
            var list = DC.EH.FuncTExpression(func);
            foreach (var dic in list)
            {
                dic.Option = OptionEnum.ColumnAs;
                DC.DPH.AddParameter(dic);
            }
        }

        internal void SelectMHandle<M, VM>(Expression<Func<M, VM>> func)
            where M : class
        {
            DC.Action = ActionEnum.Select;
            var list = DC.EH.FuncMFExpression(func);
            foreach (var dic in list)
            {
                dic.Option = OptionEnum.ColumnAs;
                DC.DPH.AddParameter(dic);
            }
        }

        /**********************************************************************************************************/

        internal void CreateMHandle<M>(M m)
        {
            DC.Option = OptionEnum.Insert;
            SetInsertValue(m, 0);
        }
        internal void CreateMHandle<M>(IEnumerable<M> mList)
        {
            var i = 0;
            DC.Option = OptionEnum.InsertTVP;
            foreach (var m in mList)
            {
                SetInsertValue(m, i);
                i++;
            }
        }

        /**********************************************************************************************************/

        internal Impler(Context dc)
            : base(dc)
        {
            DC.IP = this;
            DC.DPH.SetParameter();
        }
    }
}
