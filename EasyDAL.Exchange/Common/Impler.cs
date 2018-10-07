using System;
using System.Data;
using System.Linq;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;

namespace Yunyong.DataExchange.Common
{
    internal abstract class Impler
        : Operator
    {
        private void GetDbVal(DicModelUI ui,DicModelDB db,Type realType)
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
                if (realType == typeof(bool)
                     || realType == typeof(bool?))
                {
                    var para = DC.PPH.BoolParamHandle(db.ColumnType, ui);
                    db.DbValue = para.Value;
                    db.DbType = para.DbType;
                }
                else if (realType == typeof(short)
                        || realType == typeof(short?))
                {
                    db.DbValue = ui.CsValue;//.ToShort();
                    db.DbType = DbType.Int16;
                }
                else if (realType == typeof(int)
                        || realType == typeof(int?))
                {
                    db.DbValue = ui.CsValue;//.ToInt();
                    db.DbType = DbType.Int32;
                }
                else if (realType == typeof(long)
                        || realType == typeof(long?))
                {
                    db.DbValue = ui.CsValue;//.ToLong();
                    db.DbType = DbType.Int64;
                }
                else if (realType.IsEnum)
                {
                    var para = DC.PPH.EnumParamHandle(db.ColumnType, ui,realType);
                    db.DbValue = para.Value;
                    db.DbType = para.DbType;
                }
                else if(realType.IsGenericType
                    && realType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var type = realType.GetGenericArguments()[0];
                    GetDbVal(ui, db, type);
                }
                else
                {
                    db.DbValue = ui.CsValue;
                    db.DbType = null;
                }
            }

        }
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
                    db.ClassFullName = ui.ClassFullName;
                    db.Crud = ui.Crud;
                    db.Action = ui.Action;
                    db.Option = ui.Option;
                    db.Compare = ui.Compare;

                    //
                    db.TableOne = ui.TableOne;
                    db.TableAliasOne = ui.TableAliasOne;
                    db.ColumnOne = ui.ColumnOne;
                    db.KeyTwo = ui.KeyTwo;
                    db.AliasTwo = ui.AliasTwo;
                    db.ColumnAlias = ui.ColumnAlias;
                    db.Param = ui.Param;
                    db.ParamRaw = ui.ParamRaw;
                    db.TvpIndex = ui.TvpIndex;
                    GetDbVal(ui, db, ui.ValueType);
                    DC.DbConditions.Add(db);
                }
            }
        }

        internal Impler(Context dc)
            : base(dc)
        {
            DC.IP = this;
            ConvertDic();
        }
    }
}
