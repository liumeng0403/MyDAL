using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.ModelTools;
using System;
using System.Data;
using System.Linq;

namespace MyDAL.Core.Helper
{
    internal class ParameterHelper
    {

        private Context DC { get; set; }
        private static object DbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
        internal ParameterHelper(Context dc)
        {
            DC = dc;
        }

        /**********************************************************************************************************/

        internal DbType GetType(ParamTypeEnum colType, Type realType, Context dc)
        {
            if (realType == null)
            {
                return DbType.AnsiString;
            }
            else if (XConfig.TypeFuncs.TryGetValue(realType, out var func))
            {
                return func(dc, colType);
            }
            else if (realType.IsEnum)
            {
                return GetType(colType, Enum.GetUnderlyingType(realType), dc);
            }
            else if (realType.FullName == XConfig.TC.LinqBinary)
            {
                return DbType.Binary;
            }
            else if (XConfig.TC.IEnumerableT.IsAssignableFrom(realType))
            {
                return (DbType)(-1);
            }
            else
            {
                throw dc.Exception(XConfig.EC._032, $"不支持的字段参数类型:[[{realType}]]!");
            }
        }
        private static ParamTypeEnum GetColType(string colType, Context dc)
        {
            if (!XConfig.MySQLTypes.TryGetValue(colType.ToLower(), out var type))
            {
                throw dc.Exception(XConfig.EC._031, colType);
            }
            return type;
        }

        /*************************************************************************************************************************************/


        internal static ParamInfo EnumParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
            var val = default(object);

            //
            if (type == DbType.Int32)
            {
                if (ui.CsValue is string)
                {
                    val = (int)(Enum.Parse(realType, ui.CsValue?.ToString(), true));
                }
                else if (ui.CsValue == null)
                {
                    val = null;
                }
                else if (ui.CsValue is int)
                {
                    val = ui.CsValue.ToInt();
                }
                else
                {
                    val = (int)(ui.CsValue);
                }
            }
            else
            {
                throw dc.Exception(XConfig.EC._051, $"不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal static ParamInfo NullParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
            return dc.PH.GetDefault(ui.Param, null, type);
        }

        /*************************************************************************************************************************************/

        internal ParamInfo GetDefault(string paraName, object dbVal, DbType dbType)
        {
            return new ParamInfo
            {
                Name = paraName,
                Value = DbNull(dbVal),
                Type = dbType,
                ParameterDirection = ParameterDirection.Input,
                Size = null,
                Precision = null,
                Scale = null
            };
        }
        internal void GetParamInfo(DicParam ui, Type realType)
        {

            //
            if (DC.IsParameter(ui.Action))
            {
                //
                if (ui.Option == OptionEnum.OneEqualOne)
                { }
                else if (ui.Action == ActionEnum.SQL)
                {
                    ui.ColumnType = ui.ParamUI.ParamType;
                }
                else
                {
                    var tbm = DC.XC.GetTableModel(ui.TbMType);
                    var col = tbm.TbCols.FirstOrDefault(it => it.ColumnName.Equals(ui.TbCol, StringComparison.OrdinalIgnoreCase));
                    if (col != null)
                    {
                        ui.ColumnType = GetColType(col.DataType, DC);
                    }
                }

                //
                var para = default(ParamInfo);
                if (realType == null
                    && ui.Action == ActionEnum.SQL)
                {
                    para = NullParam(ui, realType, DC);
                }
                else if (XConfig.ParamFuncs.TryGetValue(realType, out var func))
                {
                    para = func(ui, realType, DC);
                }
                else if (realType.IsEnum)
                {
                    para = EnumParam(ui, realType, DC);
                }
                else if (realType.IsNullable())
                {
                    var type = Nullable.GetUnderlyingType(realType);
                    if (ui.CsValue == null)
                    {
                        para = NullParam(ui, type, DC);
                    }
                    else
                    {
                        GetParamInfo(ui, type);
                        return;
                    }
                }
                else
                {
                    throw DC.Exception(XConfig.EC._033, $"不支持的字段参数类型:[[{realType}]]!");
                }

                //
                ui.ParamInfo = para;
            }

        }

    }
}
