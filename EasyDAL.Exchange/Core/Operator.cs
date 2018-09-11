using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public abstract class Operator
    {
        
        internal Operator()
        {
        }

        internal DbContext DC { get; set; }

        internal void SetChangeHandle<M, F>(Expression<Func<M, F>> func, F modVal, ActionEnum action, OptionEnum option)
        {
            var key = DC.EH.ExpressionHandle(func);
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
                KeyOne = key,
                Param = key,
                ParamRaw=key,
                Value = val,
                Option = option,
                Action = action,
                Crud = CrudTypeEnum.Update
            });
        }

        private List<(string key,string param,string val,Type valType,string colType)> GetKPV<M>(object objx)
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
            var result = new List<(string key, string param, string val,Type valType,string colType)>();
            var columns = ( DC.SC.GetColumnInfos<M>(DC)).GetAwaiter().GetResult();
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
        protected void DynamicSetHandle<M>(object mSet)
        {
            var tuples = GetKPV<M>(mSet);
            foreach (var tp in tuples)
            {             
                DC.AddConditions(new DicModel
                {
                    KeyOne = tp.key,
                    Param = tp.param,
                    ParamRaw=tp.param,
                    Value = tp.val,
                    Action = ActionEnum.Update,
                    Option = OptionEnum.Set,
                    Crud = CrudTypeEnum.Update
                });
            }
        }
        
        protected void WhereHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        protected void DynamicWhereHandle<M>(object mWhere)
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
                    KeyOne = tp.key,
                    Param = tp.param,
                    ParamRaw=tp.param,
                    Value = tp.val,
                    ValueType=tp.valType,
                    ColumnType=tp.colType,
                    Action = action,
                    Option = OptionEnum.Equal,
                    Crud = CrudTypeEnum.Query
                });
            }
        }

        protected void AndHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        protected void OrHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        protected void OptionOrderByHandle(PagingQueryOption option)
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
                            KeyOne = item.Field,
                            Action = ActionEnum.OrderBy,
                            Option = op
                        });
                    }
                }
            }
        }

        protected async Task<VM> QueryFirstOrDefaultAsyncHandle<DM,VM>()
        {
            return await SqlHelper.QueryFirstOrDefaultAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(SqlTypeEnum.QueryFirstOrDefaultAsync)[0],
                DC.GetParameters());
        }

        protected async Task<List<VM>> QueryListAsyncHandle<DM,VM>()
        {
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(SqlTypeEnum.QueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

        internal async Task<PagingList<VM>> QueryPagingListAsyncHandle<DM,VM>(int pageIndex, int pageSize,SqlTypeEnum sqlType)
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
                DC.SqlProvider.GetSQL<DM>(SqlTypeEnum.QueryAllAsync)[0],
                DC.GetParameters())).ToList();
        }
    }
}
