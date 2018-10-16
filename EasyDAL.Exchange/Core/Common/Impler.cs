using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.ExpressionX;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange.Core.Common
{
    internal abstract class Impler
        : Operator
    {

        /**********************************************************************************************************/

        private void GetDbVal(DicModelUI ui, DicModelDB db, Type realType)
        {

            //
            if (DC.IsParameter(ui))
            {
                //
                if (ui.Option != OptionEnum.OneEqualOne)
                {
                    var columns = DC.SC.GetColumnInfos(DC.SC.GetKey(ui.ClassFullName, DC.Conn.Database));
                    var col = columns.FirstOrDefault(it => it.ColumnName.Equals(ui.ColumnOne, StringComparison.OrdinalIgnoreCase));
                    if (col != null)
                    {
                        db.ColumnType = col.DataType;
                    }
                }

                //
                var para = default(ParamInfo);
                if (realType == typeof(bool))
                {
                    para = DC.PPH.BoolParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(byte))
                {
                    para = DC.PPH.ByteParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(char))
                {
                    para = DC.PPH.CharParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(decimal))
                {
                    para = DC.PPH.DecimalParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(double))
                {
                    para = DC.PPH.DoubleParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(float))
                {
                    para = DC.PPH.FloatParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(int))
                {
                    para = DC.PPH.IntParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(long))
                {
                    para = DC.PPH.LongParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(sbyte))
                {
                    para = DC.PPH.SbyteParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(short))
                {
                    para = DC.PPH.ShortParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(uint))
                {
                    para = DC.PPH.UintParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(ulong))
                {
                    para = DC.PPH.UlongParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(ushort))
                {
                    para = DC.PPH.UshortParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(string))
                {
                    para = DC.PPH.StringParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(DateTime))
                {
                    para = DC.PPH.DateTimeParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(TimeSpan))
                {
                    para = DC.PPH.TimeSpanParam(db.ColumnType, ui, realType);
                }
                else if (realType == typeof(Guid))
                {
                    para = DC.PPH.GuidParam(db.ColumnType, ui, realType);
                }
                else if (realType.IsEnum)
                {
                    para = DC.PPH.EnumParam(db.ColumnType, ui, realType);
                }
                else if (realType.IsGenericType
                    && realType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var type = realType.GetGenericArguments()[0];
                    if (ui.CsValue == null)
                    {
                        para = DC.PPH.NullParam(db.ColumnType, ui, type);
                    }
                    else
                    {
                        GetDbVal(ui, db, type);
                        return;
                    }
                }
                else
                {
                    throw new Exception($"不支持的字段参数类型:[[{realType}]]!");
                }

                //
                db.DbValue = para.Value;
                db.DbType = para.DbType;
            }

        }

        /**********************************************************************************************************/

        internal void ConvertDic()
        {
            if (DC.UiConditions != null)
            {
                foreach (var ui in DC.UiConditions)
                {
                    if (DC.DbConditions.Any(dm => dm.ID == ui.ID))
                    {
                        continue;
                    }

                    var db = new DicModelDB();

                    //
                    db.ID = ui.ID;
                    //db.ClassFullName = ui.ClassFullName;
                    db.Crud = ui.Crud;
                    db.Action = ui.Action;
                    db.Option = ui.Option;
                    db.Compare = ui.Compare;

                    //
                    if (ui.ClassFullName.IsNullStr())
                    {
                        db.Key = string.Empty;
                    }
                    else
                    {
                        db.Key = DC.SC.GetKey(ui.ClassFullName, DC.Conn.Database);
                        db.TableOne = DC.SC.GetModelTableName(db.Key); //ui.TableOne;
                    }
                    db.TableAliasOne = ui.TableAliasOne;
                    db.ColumnOne = ui.ColumnOne;
                    db.KeyTwo = ui.ColumnTwo;
                    db.AliasTwo = ui.TableAliasTwo;
                    db.ColumnAlias = ui.ColumnOneAlias;
                    db.Param = ui.Param;
                    db.ParamRaw = ui.ParamRaw;
                    db.TvpIndex = ui.TvpIndex;
                    GetDbVal(ui, db, ui.CsType);
                    DC.DbConditions.Add(db);
                }
            }
        }

        /**********************************************************************************************************/

        protected async Task<VM> QueryFirstOrDefaultAsyncHandle<DM, VM>()
        {
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        protected async Task<List<VM>> QueryAllAsyncHandle<DM, VM>()
        {
            return (await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(UiMethodEnum.QueryAllAsync)[0],
                DC.SqlProvider.GetParameters())).ToList();
        }

        protected async Task<List<VM>> QueryListAsyncHandle<DM, VM>()
        {
            return (await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<DM>(UiMethodEnum.QueryListAsync)[0],
                DC.SqlProvider.GetParameters())).ToList();
        }

        protected async Task<PagingList<VM>> QueryPagingListAsyncHandle<DM, VM>(int pageIndex, int pageSize, UiMethodEnum sqlType)
        {
            var result = new PagingList<VM>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.SqlProvider.GetParameters();
            var sql = DC.SqlProvider.GetSQL<DM>(sqlType, result.PageIndex, result.PageSize);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await DC.DS.ExecuteReaderMultiRowAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        /**********************************************************************************************************/

        internal void SelectMHandle<M>()
        {
            var vmType = typeof(M);
            var fullName = vmType.FullName;
            var vmProps = DC.GH.GetPropertyInfos(vmType);
            var tab = DC.UiConditions.FirstOrDefault(it => fullName.Equals(it.ClassFullName, StringComparison.OrdinalIgnoreCase));
            if (tab != null)
            {
                foreach (var prop in vmProps)
                {
                    DC.AddConditions(DicHandle.ColumnDic(prop.Name, tab.TableAliasOne, fullName));
                }
            }
            else
            {
                var fullNames = DC.UiConditions.Where(it => !string.IsNullOrWhiteSpace(it.ClassFullName)).Distinct();
                throw new Exception($"请使用 [[Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)]] 方法! 或者 {vmType.Name} 必须为 [[{string.Join(",", fullNames.Select(it => it.ClassName))}]] 其中之一 !");
            }
        }

        internal void SelectMHandle<VM>(Expression<Func<VM>> func)
        {
            DC.Action = ActionEnum.Select;
            var list = DC.EH.FuncMExpression(func);
            foreach (var dic in list)
            {
                dic.Option = OptionEnum.ColumnAs;
                DC.AddConditions(dic);
            }
        }

        internal void SelectMHandle<M, VM>(Expression<Func<M, VM>> func)
        {
            DC.Action = ActionEnum.Select;
            var list = DC.EH.FuncMFExpression(func);
            foreach (var dic in list)
            {
                //dic.Action = ActionEnum.Select;
                dic.Option = OptionEnum.ColumnAs;
                //dic.Crud = CrudTypeEnum.Query;
                DC.AddConditions(dic);
            }
        }

        /**********************************************************************************************************/

        internal Impler(Context dc)
            : base(dc)
        {
            DC.IP = this;
            ConvertDic();
        }
    }
}
