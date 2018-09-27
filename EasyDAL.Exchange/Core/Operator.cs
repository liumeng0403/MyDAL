using MyDAL.Common;
using MyDAL.Enums;
using MyDAL.ExpressionX;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Core
{
    public abstract class Operator
        : IObjectMethod
    {
        
        internal Operator(Context dc)
        {
            DC = dc;
            DC.OP = this;
        }

        private List<(string key, string param, string val, Type valType, string colType)> GetKPV<M>(object objx)
        {
            var list = new List<string>();
            var dic = default(IDictionary<string, object>);

            if (objx is ExpandoObject)
            {
                dic = objx as IDictionary<string, object>;
                foreach (var mp in typeof(M).GetProperties())
                {
                    foreach (var sp in dic.Keys)
                    {
                        if (mp.Name.Equals(sp, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(mp.Name);
                        }
                    }
                }
            }
            else
            {
                foreach (var mp in typeof(M).GetProperties())
                {
                    foreach (var sp in objx.GetType().GetProperties())
                    {
                        if (mp.Name.Equals(sp.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(mp.Name);
                        }
                    }
                }
            }

            //
            var result = new List<(string key, string param, string val, Type valType, string colType)>();
            var columns = DC.SC.GetColumnInfos(DC.SC.GetKey(typeof(M).FullName,DC.Conn.Database));
            foreach (var prop in list)
            {
                var val = string.Empty;
                var valType = default(Type);
                var columnType = columns.First(it => it.ColumnName.Equals(prop, StringComparison.OrdinalIgnoreCase)).DataType;
                if (objx is ExpandoObject)
                {
                    var obj = dic[prop];
                    valType = obj.GetType();
                    val = DC.GH.GetTypeValue(valType, obj);
                }
                else
                {
                    var mp = objx.GetType().GetProperty(prop);
                    valType = mp.GetType();
                    val = DC.GH.GetTypeValue(valType, mp, objx);
                }

                result.Add((prop, prop, val, valType, columnType));
            }
            return result;
        }
        
        /****************************************************************************************************************************************/

        internal Context DC { get; set; }

        /****************************************************************************************************************************************/

        internal void SetChangeHandle<M, F>(Expression<Func<M, F>> func, F modVal, ActionEnum action, OptionEnum option)
        {
            var keyDic = DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            var val = string.Empty;
            if (modVal == null)
            {
                val = null;
            }
            else
            {
                val = DC.GH.GetTypeValue(modVal.GetType(), modVal);
            }
            DC.AddConditions(new DicModel
            {
                ColumnOne = key,
                Param = key,
                ParamRaw=key,
                CsValue = val,
                ValueType=typeof(F),
                Option = option,
                Action = action,
                Crud = CrudTypeEnum.Update
            });
        }

        internal void SetDynamicHandle<M>(object mSet)
        {
            var tuples = GetKPV<M>(mSet);
            foreach (var tp in tuples)
            {             
                DC.AddConditions(new DicModel
                {
                    ColumnOne = tp.key,
                    Param = tp.param,
                    ParamRaw=tp.param,
                    CsValue = tp.val,
                    ValueType=tp.valType,
                    Action = ActionEnum.Update,
                    Option = OptionEnum.Set,
                    Crud = CrudTypeEnum.Update
                });
            }
        }

        internal void WhereJoinHandle(Operator op,Expression<Func<bool>> func , ActionEnum action)
        {
            var dic = op.DC.EH.ExpressionHandle(func, action);
            dic.Crud = CrudTypeEnum.Join;
            op.DC.AddConditions(dic);
        }
        
        internal void WhereHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        internal void WhereDynamicHandle<M>(object mWhere)
        {
            var tuples = GetKPV<M>(mWhere);
            var count = 0;
            var action = ActionEnum.None;
            foreach (var tp in tuples)
            {
                count++;
                if (count == 1)
                {
                    action = ActionEnum.Where;
                }
                else
                {
                    action = ActionEnum.And;
                }
                DC.AddConditions(new DicModel
                {
                    ColumnOne = tp.key,
                    Param = tp.param,
                    ParamRaw=tp.param,
                    CsValue = tp.val,
                    ValueType=tp.valType,
                    ColumnType=tp.colType,
                    Action = action,
                    Option = OptionEnum.Compare,
                    Compare= CompareConditionEnum.Equal,
                    Crud = CrudTypeEnum.Query
                });
            }
        }

        internal void AndHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        internal void OrHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        internal void OrderByHandle<M,F>(Expression<Func<M, F>> func, OrderByEnum orderBy)
        {
            var keyDic = DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            var option = OptionEnum.None;
            switch (orderBy)
            {
                case OrderByEnum.Asc:
                    option = OptionEnum.Asc;
                    break;
                case OrderByEnum.Desc:
                    option = OptionEnum.Desc;
                    break;
            }

            DC.AddConditions(new DicModel
            {
                ColumnOne = key,
                Option = option,
                Action = ActionEnum.OrderBy,
                Crud = CrudTypeEnum.Query
            });
        }

        protected void OrderByOptionHandle(PagingQueryOption option)
        {
            if (option.OrderBys != null
              && option.OrderBys.Any())
            {
                foreach (var item in option.OrderBys)
                {
                    if (!string.IsNullOrWhiteSpace(item.Field))
                    {
                        var op = OptionEnum.None;
                        if (item.Desc)
                        {
                            op = OptionEnum.Desc;
                        }
                        else
                        {
                            op = OptionEnum.Asc;
                        }
                        DC.AddConditions(new DicModel
                        {
                            ColumnOne = item.Field,
                            Action = ActionEnum.OrderBy,
                            Option = op
                        });
                    }
                }
            }
        }

        internal void SelectMHandle<M>()
        {
            var vmType = typeof(M);
            var vmName = vmType.FullName;
            var vmProps = DC.GH.GetPropertyInfos(vmType);
            var tab = DC.Conditions.FirstOrDefault(it => vmName.Equals(it.ClassFullName, StringComparison.OrdinalIgnoreCase));
            if (tab!=null)
            {
                foreach (var prop in vmProps)
                {
                    DC.AddConditions(DicHandle.SelectColumnHandle(prop.Name, tab.TableAliasOne));
                }
            }
            else
            {
                var fullNames = DC.Conditions.Where(it => !string.IsNullOrWhiteSpace(it.ClassFullName)).Distinct();
                throw new Exception($"请使用 [[Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {vmType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.ClassName))}]] 其中之一 !");

                // 下面代码 可能有问题
                foreach (var name in fullNames.Select(it=>it.ClassFullName).Distinct())
                {
                    var type = Type.GetType(name);
                    var mProps = DC.GH.GetPropertyInfos(type);
                    foreach (var vprop in vmProps)
                    {
                        if (DC.Conditions.Any(it => vprop.Name.Equals(it.VmColumn, StringComparison.Ordinal)))
                        {
                            continue;
                        }

                        foreach (var mprop in mProps)
                        {
                            if (!vprop.Name.Equals(mprop.Name, StringComparison.Ordinal))
                            {
                                continue;
                            }

                            var ali = DC.Conditions.First(it => name.Equals(it.ClassFullName, StringComparison.Ordinal)).TableAliasOne;
                            DC.AddConditions(DicHandle.SelectColumnHandle(vprop.Name,ali));
                        }
                    }
                }
            }
        }

        internal void SelectMHandle<VM>(Expression<Func<VM>> func)
        {
            var list = DC.EH.ExpressionHandle(func);
            foreach (var dic in list)
            {
                dic.Action = ActionEnum.Select;
                dic.Option = OptionEnum.ColumnAs;
                dic.Crud = CrudTypeEnum.Join;
                DC.AddConditions(dic);
            }
        }

        internal void SelectMHandle<M,VM>(Expression<Func<M,VM>> func)
        {
            var list = DC.EH.ExpressionHandle(func);
            foreach (var dic in list)
            {
                dic.Action = ActionEnum.Select;
                dic.Option = OptionEnum.ColumnAs;
                dic.Crud = CrudTypeEnum.Query;
                DC.AddConditions(dic);
            }
        }

        /****************************************************************************************************************************************/

        protected async Task<VM> QueryFirstOrDefaultAsyncHandle<DM,VM>()
        {
            return await SqlHelper.QueryFirstOrDefaultAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.GetParameters());
        }

        protected async Task<List<VM>> QueryListAsyncHandle<DM,VM>()
        {
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(UiMethodEnum.QueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

        internal async Task<PagingList<VM>> QueryPagingListAsyncHandle<DM,VM>(int pageIndex, int pageSize, UiMethodEnum sqlType)
        {
            var result = new PagingList<VM>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<DM>(sqlType, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<long>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        protected async Task<List<VM>> QueryAllAsyncHandle<DM,VM>()
        {
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(UiMethodEnum.QueryAllAsync)[0],
                DC.GetParameters())).ToList();
        }
    }
}
