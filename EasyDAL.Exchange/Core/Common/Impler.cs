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
                if (realType == XConfig.Bool)
                {
                    para = DC.PH.BoolParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Byte)
                {
                    para = DC.PH.ByteParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Char)
                {
                    para = DC.PH.CharParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Decimal)
                {
                    para = DC.PH.DecimalParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Double)
                {
                    para = DC.PH.DoubleParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Float)
                {
                    para = DC.PH.FloatParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Int)
                {
                    para = DC.PH.IntParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Long)
                {
                    para = DC.PH.LongParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Sbyte)
                {
                    para = DC.PH.SbyteParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Short)
                {
                    para = DC.PH.ShortParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Uint)
                {
                    para = DC.PH.UintParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Ulong)
                {
                    para = DC.PH.UlongParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Ushort)
                {
                    para = DC.PH.UshortParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.String)
                {
                    para = DC.PH.StringParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.DateTime)
                {
                    para = DC.PH.DateTimeParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.TimeSpan)
                {
                    para = DC.PH.TimeSpanParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Guid)
                {
                    para = DC.PH.GuidParam(db.ColumnType, ui, realType);
                }
                else if (realType.IsEnum)
                {
                    para = DC.PH.EnumParam(db.ColumnType, ui, realType);
                }
                else if (realType.IsGenericType
                    && realType.GetGenericTypeDefinition() == XConfig.NullableT)
                {
                    var type = realType.GetGenericArguments()[0];
                    if (ui.CsValue == null)
                    {
                        para = DC.PH.NullParam(db.ColumnType, ui, type);
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
